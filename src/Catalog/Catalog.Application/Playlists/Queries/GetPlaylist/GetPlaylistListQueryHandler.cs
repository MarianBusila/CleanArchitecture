using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Common.System.Collections.Generic;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistListQueryHandler : IRequestHandler<GetPlaylistListQuery, IEnumerable<PlaylistDetail>>
    {

        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistFilter _playlistFilter;
        private readonly IMapper _mapper;

        public GetPlaylistListQueryHandler(IPlaylistRepository playlistRepository, IPlaylistFilter playlistFilter, IMapper mapper)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _playlistFilter = playlistFilter ?? throw new ArgumentNullException(nameof(playlistFilter));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlaylistDetail>> Handle(GetPlaylistListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            IPlaylistFilter filter = _playlistFilter
                .WhereTrackIdEquals(request.TrackId)
                .WhereNameLike(request.Name);

            IPagedCollection<Playlist> playlistsDomain = await _playlistRepository.GetPlaylists(filter, request.PageNumber, request.PageSize);

            var playlists = _mapper.Map<IReadOnlyList<PlaylistDetail>>(playlistsDomain);
            return new PagedCollection<PlaylistDetail>(playlists, playlistsDomain.ItemCount, playlistsDomain.CurrentPageNumber, playlistsDomain.PageSize);
        }

    }
}
