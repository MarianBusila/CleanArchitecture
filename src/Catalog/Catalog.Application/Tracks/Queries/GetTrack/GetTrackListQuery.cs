﻿using System;
using System.Collections.Generic;
using Catalog.Application.Tracks.Queries.GetTrack.Filters;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using MediatR;

namespace Catalog.Application.Tracks.Queries.GetTrack
{
    public sealed class GetTrackListQuery : IRequest<IPagedCollection<TrackDetail>>
    {
        public int? PlaylistId { get; }

        public int PageNumber { get; }
        public int PageSize { get; }
        public string Name { get; }
        public string Composer { get; }
        public string Genre { get; }
        public string Album { get; }
        public string Artist { get; }
        public string MediaType { get; }
        public PriceFilter PriceFrom { get; }
        public PriceFilter PriceTo { get; }

        public GetTrackListQuery(TrackQuery trackQuery)
        {
            if (trackQuery is null)
                throw new ArgumentNullException(nameof(trackQuery));

            Album = trackQuery.Album;
            Artist = trackQuery.Artist;
            Composer = trackQuery.Composer;
            Genre = trackQuery.Genre;
            MediaType = trackQuery.MediaType;
            Name = trackQuery.Name;
            PageNumber = trackQuery.PageNumber;
            PageSize = trackQuery.PageSize;
            PriceFrom = trackQuery.PriceFrom == null ? null : new PriceFilter(trackQuery.PriceFrom);
            PriceTo = trackQuery.PriceTo == null ? null : new PriceFilter(trackQuery.PriceTo);

        }

        public GetTrackListQuery(int playlistId, TrackQuery trackQuery) : this(trackQuery)
        {
            PlaylistId = playlistId;
        }
    }
}
