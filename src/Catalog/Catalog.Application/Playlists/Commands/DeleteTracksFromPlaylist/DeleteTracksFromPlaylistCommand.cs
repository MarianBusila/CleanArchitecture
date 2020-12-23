using System;
using System.Collections.Generic;
using MediatR;

namespace Catalog.Application.Playlists.Commands.DeleteTracksFromPlaylist
{
    public sealed class DeleteTracksFromPlaylistCommand : IRequest
    {

        public int PlaylistId { get; }

        public IReadOnlyCollection<int> TrackIds { get; }


        public DeleteTracksFromPlaylistCommand(int playlistId, IReadOnlyCollection<int> trackIds)
        {
            PlaylistId = playlistId;
            TrackIds = trackIds ?? throw new ArgumentNullException(nameof(trackIds));
        }
    }
}
