using MediatR;

namespace Catalog.Application.Playlists.Commands.DeletePlaylist
{
    public sealed class DeletePlaylistCommand : IRequest
    {
        public int PlaylistId { get; }

        public DeletePlaylistCommand(int playlistId)
        {
            PlaylistId = playlistId;
        }
    }
}
