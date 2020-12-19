using System.Collections.Generic;
using Catalog.Application.Playlists.Commands.UpdatePlaylist.Models;
using MediatR;

namespace Catalog.Application.Playlists.Commands.UpdatePlaylist
{
    public sealed class UpdatePlaylistCommand : IRequest
    {
        public string Name { get; }
        public int PlaylistId { get; }
        public IReadOnlyCollection<int> TrackIds { get; }

        public UpdatePlaylistCommand(int playlistId, PlaylistForUpdate playlist)
        {
            Name = playlist.Name;
            PlaylistId = playlistId;
            TrackIds = playlist.TrackIds;
        }
    }
}
