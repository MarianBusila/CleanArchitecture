using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Mappers
{
    public sealed class AlbumMapperProfile : Profile
    {

        public AlbumMapperProfile()
        {
            CreateMap<Album, TrackAlbum>();
        }
    }
}
