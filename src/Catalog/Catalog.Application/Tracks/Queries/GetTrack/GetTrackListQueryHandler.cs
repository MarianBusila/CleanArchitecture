using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using MediatR;

namespace Catalog.Application.Tracks.Queries.GetTrack
{
    public sealed class GetTrackListQueryHandler : IRequestHandler<GetTrackListQuery, IPagedCollection<TrackDetail>>
    {

        public GetTrackListQueryHandler()
        {
            
        }

        public Task<IPagedCollection<TrackDetail>> Handle(GetTrackListQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

    }
}
