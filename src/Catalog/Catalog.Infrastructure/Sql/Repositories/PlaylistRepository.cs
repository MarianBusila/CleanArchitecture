using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Playlist>> GetPlaylists(IPlaylistFilter filter)
        {
            var linqPlaylistFilter = filter as LinqPlaylistFilter;
            if (linqPlaylistFilter is null)
                throw new ArgumentException("Sql playlist repository expects a linq filter");

            return await _playlistDbContext
                .Playlists
                .Where(linqPlaylistFilter.Filter)
                .Include(e => e.PlaylistTracks)
                .ToListAsync();
        }

    }
}
