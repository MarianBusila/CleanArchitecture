using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Application.Repositories;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistListQueryHandler : IRequestHandler<GetPlaylistListQuery, IEnumerable<PlaylistDetail>>
    {

        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistFilter _playlistFilter;

        public GetPlaylistListQueryHandler(IPlaylistRepository playlistRepository, IPlaylistFilter playlistFilter)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _playlistFilter = playlistFilter ?? throw new ArgumentNullException(nameof(playlistFilter));
        }

        public async Task<IEnumerable<PlaylistDetail>> Handle(GetPlaylistListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var filter = _playlistFilter
                .WhereTrackIdEquals(request.TrackId)
                .WhereNameLike(request.Name);

            var playlistsDomain = await _playlistRepository.GetPlaylists(filter);

            // TODO add mapper

            return  new List<PlaylistDetail>
            {
                new PlaylistDetail
                {
                    Id = 1,
                    Name = "Playlist1",
                    TrackCount = 12
                }
            };
        }

    }
}
