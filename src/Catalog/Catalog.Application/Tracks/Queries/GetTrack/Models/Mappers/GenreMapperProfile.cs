using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Mappers
{
    public sealed class GenreMapperProfile : Profile
    {

        public GenreMapperProfile()
        {
            CreateMap<Genre, TrackGenre>();
        }
    }
}
