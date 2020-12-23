
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Domain.Models;

namespace Catalog.Application.Repositories
{
    public interface ICatalogRepository
    {

        Task<IPagedCollection<Playlist>> GetPlaylists(IPlaylistFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Playlist> GetPlaylist(int playlistId, CancellationToken cancellationToken);

        Task CreatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken);

        Task UpdatePlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken);

        Task DeletePlaylist(Playlist playlist, CancellationToken cancellationToken);

        Task AddTracksToPlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken);

        Task DeleteTracksFromPlaylist(Playlist playlist, IReadOnlyCollection<int> trackIds, CancellationToken cancellationToken);

    }
}
