using System;
using System.Collections.Generic;
using MediatR;

namespace Catalog.Application.Playlists.Commands.AddTracksToPlaylist
{
    public sealed class AddTracksToPlaylistCommand : IRequest
    {

        public int PlaylistId { get; }

        public IReadOnlyCollection<int> TrackIds { get; }


        public AddTracksToPlaylistCommand(int playlistId, IReadOnlyCollection<int> trackIds)
        {
            PlaylistId = playlistId;
            TrackIds = trackIds ?? throw new ArgumentNullException(nameof(trackIds));
        }
    }
}
