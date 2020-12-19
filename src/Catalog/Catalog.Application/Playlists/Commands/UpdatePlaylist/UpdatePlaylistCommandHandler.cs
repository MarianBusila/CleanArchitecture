
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using Catalog.Application.Playlists.Commands.UpdatePlaylist;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Common.Microsoft.EntityFrameworkCore;
using MediatR;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist
{
    public sealed class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;

        public UpdatePlaylistCommandHandler(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Playlist playlistDomain = await _playlistRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            if(playlistDomain == null)
                throw new EntityNotFoundException($"A playlist having id '{request.PlaylistId}' could not be found");

            playlistDomain.Name = request.Name;
            await _playlistRepository.UpdatePlaylist(playlistDomain, request.TrackIds, cancellationToken);
            return Unit.Value;
        }

    }
}
