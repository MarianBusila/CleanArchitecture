using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Mappers
{
    public sealed class MediaTypeMapperProfile : Profile
    {

        public MediaTypeMapperProfile()
        {
            CreateMap<MediaType, TrackMediaType>();
        }
    }
}
