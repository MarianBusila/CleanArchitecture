using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Repositories;
using Catalog.Application.Tracks.Queries.GetTrack.Filters;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using Catalog.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Tracks.Queries.GetTrack
{
    public sealed class GetTrackListQueryHandler : IRequestHandler<GetTrackListQuery, IPagedCollection<TrackDetail>>
    {

        private readonly ICatalogRepository _catalogRepository;
        private readonly ITrackFilter _trackFilter;
        private readonly IMapper _mapper;
        private ILogger<GetTrackListQueryHandler> _logger;

        public GetTrackListQueryHandler(ICatalogRepository catalogRepository, ITrackFilter trackFilter, IMapper mapper, ILogger<GetTrackListQueryHandler> logger)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _trackFilter = trackFilter ?? throw new ArgumentNullException(nameof(trackFilter));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IPagedCollection<TrackDetail>> Handle(GetTrackListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Handle {QueryType} for tracks like {TrackName} with page number {PageNumber} and page size {PageSize}", nameof(GetTrackListQuery), request.Name, request.PageNumber, request.PageSize);
            ITrackFilter filter = _trackFilter
                .WhereNameLike(request.Name)
                .WhereAlbumLike(request.Album)
                .WhereArtistLike(request.Artist)
                .WhereComposerLike(request.Composer)
                .WhereGenreLike(request.Genre)
                .WhereMediaTypeLike(request.MediaType)
                .WherePlaylistIdEquals(request.PlaylistId)
                .WherePrice(request.PriceFrom, request.PriceTo);

            IPagedCollection<Track> tracksDomain = await _catalogRepository.GetTracks(filter, request.PageNumber, request.PageSize, cancellationToken);

            var tracks = _mapper.Map<IReadOnlyList<TrackDetail>>(tracksDomain);
            return new PagedCollection<TrackDetail>(tracks, tracksDomain.ItemCount, tracksDomain.CurrentPageNumber, tracksDomain.PageSize);
        }

    }
}
