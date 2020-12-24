using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Catalog.Domain.Models;
using Humanizer;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Mappers
{
    public sealed class TrackMapperProfile : Profile
    {

        public TrackMapperProfile()
        {
            CreateMap<Track, TrackDetail>()
                .ForMember(destination =>
                        destination.Artist,
                    options => options.MapFrom(source => source.Album!.Artist))
                .ForMember(destination =>
                        destination.SizeInBytes,
                    options => options.MapFrom(source => source.Bytes))
                .ForMember(destination =>
                        destination.Size,
                    options => options.MapFrom(source => (source.Bytes).Bytes().Humanize("MB", CultureInfo.InvariantCulture)))
                .ForMember(destination =>
                        destination.TimeInMilliseconds,
                    options => options.MapFrom(source => source.Milliseconds))
                .ForMember(destination =>
                        destination.Time,
                    options => options.MapFrom(source => HumanizeMilliseconds(source.Milliseconds)));


            CreateMap<IPagedCollection<Track>, List<TrackDetail>>();
        }

        // This method was extracted due to the mapping expression tree
        // not allowing methods with optional arguments
        private static string HumanizeMilliseconds(int milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds).Humanize();
        }
    }
}
