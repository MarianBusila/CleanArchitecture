using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistListQueryHandler : IRequestHandler<GetPlaylistListQuery, IEnumerable<PlaylistDetail>>
    {

        public GetPlaylistListQueryHandler()
        {
            
        }

        public Task<IEnumerable<PlaylistDetail>> Handle(GetPlaylistListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // TODO add implementation

            IEnumerable<PlaylistDetail> playlists = new List<PlaylistDetail>
            {
                new PlaylistDetail
                {
                    Id = 1,
                    Name = "Playlist1",
                    TrackCount = 12
                }
            };
            return Task.FromResult(playlists);
        }

    }
}
