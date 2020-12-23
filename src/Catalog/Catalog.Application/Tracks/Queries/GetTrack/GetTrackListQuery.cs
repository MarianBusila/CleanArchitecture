using System;
using System.Collections.Generic;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using MediatR;

namespace Catalog.Application.Tracks.Queries.GetTrack
{
    public sealed class GetTrackListQuery : IRequest<IPagedCollection<TrackDetail>>
    {
        // TODO define properties
        public GetTrackListQuery(TrackQuery trackQuery)
        {
            if (trackQuery is null)
                throw new ArgumentNullException(nameof(trackQuery));
        }
    }
}
