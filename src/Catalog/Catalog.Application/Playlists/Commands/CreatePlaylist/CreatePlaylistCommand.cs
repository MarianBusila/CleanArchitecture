using System.Collections.Generic;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using MediatR;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist
{
    public sealed class CreatePlaylistCommand : IRequest<PlaylistFromCreate>
    {
        public string Name { get; }
        public IReadOnlyCollection<int> TrackIds { get; }
        
        public CreatePlaylistCommand(PlaylistForCreate playlist)
        {
            if (playlist is null)
                throw new System.ArgumentNullException(nameof(playlist));

            Name = playlist.Name;
            TrackIds = playlist.TrackIds;
        }
    }
}
