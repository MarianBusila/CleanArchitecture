using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Catalog.Infrastructure.Sql.Filters;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {

        private readonly PlaylistDbContext _playlistDbContext;

        public PlaylistRepository(PlaylistDbContext playlistDbContext)
        {
            _playlistDbContext = playlistDbContext ?? throw new ArgumentNullException(nameof(playlistDbContext));
        }

        public async Task<IPagedCollection<Playlist>> GetPlaylists(IPlaylistFilter filter, int pageNumber, int pageSize)
        {
            var linqPlaylistFilter = filter as LinqPlaylistFilter;
            if (linqPlaylistFilter is null)
                throw new ArgumentException("Sql playlist repository expects a linq filter");

            return await _playlistDbContext
                .Playlists
                .TagWithQueryName(nameof(GetPlaylists))
                .Where(linqPlaylistFilter.Filter)
                .Include(e => e.PlaylistTracks)
                .ToPagedCollectionAsync(pageNumber, pageSize);
        }

        public async Task<Playlist> GetPlaylist(int playlistId)
        {
            return await _playlistDbContext
                .Playlists
                .TagWithQueryName(nameof(GetPlaylist))
                .Where(playlist => playlist.Id == playlistId)
                .Include(e => e.PlaylistTracks)
                .FirstOrDefaultAsync();
        }

        public async Task CreatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken)
        {
            List<int> normalizedTrackIds = await NormalizeTrackIds(trackIds, cancellationToken);

            normalizedTrackIds.ForEach(trackId => playlist.PlaylistTracks.Add(new PlaylistTrack {TrackId = trackId}));
            _playlistDbContext.Playlists.Add(playlist);
            var entriesSaved = await _playlistDbContext.SaveChangesAsync(cancellationToken);
            var expectedEntriesSaved = trackIds.Count + 1;
            if (entriesSaved != expectedEntriesSaved)
                throw new DbUpdateException($"While adding a new playlist, received '{entriesSaved}' saved entries, but expected '{expectedEntriesSaved}'");

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
                var trackIdsFromDb = await _playlistDbContext
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
