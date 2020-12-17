using AutoMapper;
using Catalog.Domain.Models;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist.Models.Mappers
{
    public sealed class PlaylistMapperProfile : Profile
    {

        public PlaylistMapperProfile()
        {
            CreateMap<CreatePlaylistCommand, Playlist>();
            CreateMap<Playlist, PlaylistFromCreate>();
        }
    }
}
