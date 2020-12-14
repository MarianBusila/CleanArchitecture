using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Playlists.Queries.GetPlaylist.Models.Mappers
{
    public sealed class PlaylistMapperProfile : Profile
    {

        public PlaylistMapperProfile()
        {
            CreateMap<Playlist, PlaylistDetail>()
                .ForMember(destination => destination.TrackCount, options => options.MapFrom(source => source.PlaylistTracks.Count));

            CreateMap<IEnumerable<Playlist>, PlaylistDetail>();
        }
        
    }
}
