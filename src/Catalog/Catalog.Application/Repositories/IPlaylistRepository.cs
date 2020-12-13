
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Domain.Models;

namespace Catalog.Application.Repositories
{
    public interface IPlaylistRepository
    {

        Task<IEnumerable<Playlist>> GetPlaylists(IPlaylistFilter filter);

    }
}
