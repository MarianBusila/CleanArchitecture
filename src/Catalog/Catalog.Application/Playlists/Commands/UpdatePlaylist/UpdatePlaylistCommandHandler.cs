using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Commands.UpdatePlaylist;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist
{
    public sealed class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public UpdatePlaylistCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Playlist playlistDomain = await _catalogRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            if(playlistDomain == null)
                throw new EntityNotFoundException($"A playlist having id '{request.PlaylistId}' could not be found");

            playlistDomain.Name = request.Name;
            await _catalogRepository.UpdatePlaylist(playlistDomain, request.TrackIds, cancellationToken);
            return Unit.Value;
        }

    }
}
