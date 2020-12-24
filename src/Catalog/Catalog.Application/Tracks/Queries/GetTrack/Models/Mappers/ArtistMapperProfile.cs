using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Mappers
{
    public sealed class ArtistMapperProfile : Profile
    {

        public ArtistMapperProfile()
        {
            CreateMap<Artist, TrackArtist>();
        }
    }
}
