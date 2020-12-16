using AutoMapper;
using Catalog.Domain.Models;
using System.Collections.Generic;

namespace Catalog.Application.Playlists.Queries.GetPlaylist.Models.Mappers
{
    public sealed class PlaylistMapperProfile : Profile
    {

        public PlaylistMapperProfile()
        {
            CreateMap<Playlist, PlaylistDetail>()
                .ForMember(destination => destination.TrackCount, options => options.MapFrom(source => source.PlaylistTracks.Count));

            CreateMap<IPagedCollection<Playlist>, PlaylistDetail>();
        }
        
    }
}
