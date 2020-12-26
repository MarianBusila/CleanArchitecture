using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Repositories;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using Catalog.Domain.Models;
using MediatR;

namespace Catalog.Application.Tracks.Queries.GetTrack
{
    public sealed class GetTrackListQueryHandler : IRequestHandler<GetTrackListQuery, IPagedCollection<TrackDetail>>
    {

        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetTrackListQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IPagedCollection<TrackDetail>> Handle(GetTrackListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            IPagedCollection<Track> tracksDomain = await _catalogRepository.GetTracks(request.PageNumber, request.PageSize, cancellationToken);

            var tracks = _mapper.Map<IReadOnlyList<TrackDetail>>(tracksDomain);
            return new PagedCollection<TrackDetail>(tracks, tracksDomain.ItemCount, tracksDomain.CurrentPageNumber, tracksDomain.PageSize);
        }

    }
}
