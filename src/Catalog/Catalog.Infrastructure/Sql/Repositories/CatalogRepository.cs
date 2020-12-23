using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Repositories;
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

        public async Task<IPagedCollection<Playlist>> GetPlaylists(IPlaylistFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var linqPlaylistFilter = filter as LinqPlaylistFilter;
            if (linqPlaylistFilter is null)
                throw new ArgumentException("Sql playlist repository expects a linq filter");

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
