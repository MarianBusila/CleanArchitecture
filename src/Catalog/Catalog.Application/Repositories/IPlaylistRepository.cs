
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Domain.Models;

namespace Catalog.Application.Repositories
{
    public interface IPlaylistRepository
    {

        Task<IPagedCollection<Playlist>> GetPlaylists(IPlaylistFilter filter, int pageNumber, int pageSize);
        Task<Playlist> GetPlaylist(int playlistId);

        Task CreatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken);

    }
}
