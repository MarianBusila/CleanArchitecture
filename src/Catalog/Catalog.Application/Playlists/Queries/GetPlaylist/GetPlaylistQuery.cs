using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistQuery : IRequest<PlaylistDetail>
    {
        public int PlaylistId { get; }

        public GetPlaylistQuery(int playlistId)
        {
            PlaylistId = playlistId;
        }

    }
}