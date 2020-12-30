using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistListQueryHandler : IRequestHandler<GetPlaylistListQuery, IPagedCollection<PlaylistDetail>>
    {

        private readonly ICatalogRepository _catalogRepository;
        private readonly IPlaylistFilter _playlistFilter;
        private readonly IMapper _mapper;
        private ILogger<GetPlaylistListQueryHandler> _logger;

        public GetPlaylistListQueryHandler(ICatalogRepository catalogRepository, IPlaylistFilter playlistFilter, IMapper mapper, ILogger<GetPlaylistListQueryHandler> logger)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _playlistFilter = playlistFilter ?? throw new ArgumentNullException(nameof(playlistFilter));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IPagedCollection<PlaylistDetail>> Handle(GetPlaylistListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Handle {QueryType} for playlists like {PlaylistName} with page number {PageNumber} and page size {PageSize}", nameof(GetPlaylistListQuery), request.Name, request.PageNumber, request.PageSize);
            IPlaylistFilter filter = _playlistFilter
                .WhereTrackIdEquals(request.TrackId)
                .WhereNameLike(request.Name);

            IPagedCollection<Playlist> playlistsDomain = await _catalogRepository.GetPlaylists(filter, request.PageNumber, request.PageSize, cancellationToken);

            var playlists = _mapper.Map<IReadOnlyList<PlaylistDetail>>(playlistsDomain);
            return new PagedCollection<PlaylistDetail>(playlists, playlistsDomain.ItemCount, playlistsDomain.CurrentPageNumber, playlistsDomain.PageSize);
        }

    }
}
