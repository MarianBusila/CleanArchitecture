﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Repositories;
using Catalog.Application.Tracks.Queries.GetTrack.Filters;
using Catalog.Domain.Models;
using Catalog.Infrastructure.Sql.Filters;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {

        private readonly CatalogDbContext _catalogDbContext;

        public CatalogRepository(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext ?? throw new ArgumentNullException(nameof(catalogDbContext));
        }

        #region Playlists
        public async Task<IPagedCollection<Playlist>> GetPlaylists(IPlaylistFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var linqPlaylistFilter = filter as LinqPlaylistFilter;
            if (linqPlaylistFilter is null)
                throw new ArgumentException("Sql catalog repository expects a linq playlist filter");

            return await _catalogDbContext
                .Playlists
                .TagWithQueryName(nameof(GetPlaylists))
                .Where(linqPlaylistFilter.Filter)
                .Include(e => e.PlaylistTracks)
                .ToPagedCollectionAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Playlist> GetPlaylist(int playlistId, CancellationToken cancellationToken)
        {
            return await _catalogDbContext
                .Playlists
                .TagWithQueryName(nameof(GetPlaylist))
                .Where(playlist => playlist.Id == playlistId)
                .Include(e => e.PlaylistTracks)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task CreatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            List<int> normalizedTrackIds = await NormalizeTrackIds(trackIds, cancellationToken);

            normalizedTrackIds.ForEach(trackId => playlist.PlaylistTracks.Add(new PlaylistTrack {TrackId = trackId}));
            _catalogDbContext.Playlists.Add(playlist);
            var entriesSaved = await _catalogDbContext.SaveChangesAsync(cancellationToken);
            var expectedEntriesSaved = trackIds.Count + 1;
            if (entriesSaved != expectedEntriesSaved)
                throw new DbUpdateException($"While adding a new playlist, received '{entriesSaved}' saved entries, but expected '{expectedEntriesSaved}'");

        }

        public async Task UpdatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            List<int> normalizedTrackIds = await NormalizeTrackIds(trackIds, cancellationToken);

            List<PlaylistTrack> currentPlaylistTracks = await _catalogDbContext
                .PlaylistTracks
                .Where(pt => pt.PlaylistId == playlist.Id)
                .ToListAsync(cancellationToken);

            IEnumerable<PlaylistTrack> newPlaylistTracks = normalizedTrackIds.Select(trackId => new PlaylistTrack
            {
                PlaylistId = playlist.Id,
                TrackId = trackId
            });

            _catalogDbContext.PlaylistTracks.RemoveRange(currentPlaylistTracks);
            _catalogDbContext.PlaylistTracks.AddRange(newPlaylistTracks);

            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePlaylist(Playlist playlist, CancellationToken cancellationToken)
        {
            var playlistTracks = await _catalogDbContext
                .PlaylistTracks
                .Where(pt => pt.PlaylistId == playlist.Id)
                .ToListAsync(cancellationToken);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _catalogDbContext.PlaylistTracks.RemoveRange(playlistTracks);
                _catalogDbContext.Playlists.Remove(playlist);

                var entries = await _catalogDbContext.SaveChangesAsync(cancellationToken);
                if(entries != playlistTracks.Count + 1)
                    throw new DbUpdateException($"Something went wrong while attempting to remove playlist having id '{playlist.Id}'");

                scope.Complete();
            }
        }

        public async Task AddTracksToPlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            var newTrackIds = trackIds
                .Distinct()
                .OrderBy(trackId => trackId)
                .ToList();

            var existingTrackIds = await _catalogDbContext
                .PlaylistTracks
                .AsNoTracking()
                .Where(pt => pt.PlaylistId == playlist.Id && newTrackIds.Contains(pt.TrackId))
                .Select(pt => pt.TrackId)
                .ToListAsync(cancellationToken);

            var tracksForAdd = newTrackIds
                .Except(existingTrackIds)
                .Select(trackId => new PlaylistTrack { PlaylistId = playlist.Id, TrackId = trackId })
                .ToList();

            if(tracksForAdd.Any())
            {
                await _catalogDbContext.PlaylistTracks.AddRangeAsync(tracksForAdd, cancellationToken);
                await _catalogDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteTracksFromPlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            var uniqueTrackIds = trackIds
                .Distinct()
                .OrderBy(trackId => trackId)
                .ToList();

            if(uniqueTrackIds.Any())
            {
                var playlistTrackIdsFromDb = await _catalogDbContext
                    .PlaylistTracks
                    .AsNoTracking()
                    .Where(p => p.PlaylistId == playlist.Id && uniqueTrackIds.Contains(p.TrackId))
                    .Select(p => p.TrackId)
                    .OrderBy(trackId => trackId)
                    .ToListAsync(cancellationToken);

                if (!playlistTrackIdsFromDb.SequenceEqual(uniqueTrackIds))
                {
                    var missingTrackIds = string.Join(",", uniqueTrackIds.Except(playlistTrackIdsFromDb));
                    throw new EntityNotFoundException($"Tracks having ids '{missingTrackIds}' do not exist");
                }

                var playlistTracksFromDb = await _catalogDbContext
                    .PlaylistTracks
                    .Where(p => p.PlaylistId == playlist.Id && uniqueTrackIds.Contains(p.TrackId))
                    .ToListAsync(cancellationToken);
                _catalogDbContext.PlaylistTracks.RemoveRange(playlistTracksFromDb);
                await _catalogDbContext.SaveChangesAsync(cancellationToken);
            }

        }

        #endregion

        #region Tracks

        public async Task<IPagedCollection<Track>> GetTracks(ITrackFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var linqTrackFilter = filter as LinqTrackFilter;
            if (linqTrackFilter is null)
                throw new ArgumentException("Sql catalog repository expects a linq track filter");

            return await _catalogDbContext
                .Tracks
                .TagWithQueryName(nameof(GetTracks))
                .AsNoTracking()
                .Where(linqTrackFilter.Filter)
                .Include(track => track.Genre)
                .Include(track => track.Album!.Artist)
                .Include(track => track.MediaType)
                .ToPagedCollectionAsync(pageNumber, pageSize, cancellationToken);
        }

        #endregion

        private async Task<List<int>> NormalizeTrackIds(IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            var normalizedTrackIds = trackIds
                    ?.Distinct()
                    .OrderBy(trackId => trackId)
                    .ToList()
                ?? new List<int>(0);

            if (trackIds != null && trackIds.Any())
            {
                var trackIdsFromDb = await _catalogDbContext
                    .Tracks
                    .Where(track => trackIds.Contains(track.Id))
                    .Select(track => track.Id)
                    .OrderBy(trackId => trackId)
                    .ToListAsync(cancellationToken);

                if (!trackIdsFromDb.SequenceEqual(normalizedTrackIds))
                {
                    var invalidTrackIds = string.Join(",", normalizedTrackIds.Except(trackIdsFromDb));
                    throw new InvalidOperationException($"Tracks having ids '{invalidTrackIds}' do not exist");
                }
            }

            return normalizedTrackIds;
        }

    }
}
