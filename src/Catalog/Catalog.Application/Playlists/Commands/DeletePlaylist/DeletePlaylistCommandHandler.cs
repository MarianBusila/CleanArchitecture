using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Commands.UpdatePlaylist;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Common.Microsoft.EntityFrameworkCore;
using MediatR;

namespace Catalog.Application.Playlists.Commands.DeletePlaylist
{
    public sealed class DeletePlaylistCommandHandler : IRequestHandler<DeletePlaylistCommand>
    {
        private readonly ICatalogRepository _catalogRepository;

        public DeletePlaylistCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        public async Task<Unit> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Playlist playlistDomain = await _catalogRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            if(playlistDomain == null)
                throw new EntityNotFoundException($"A playlist having id '{request.PlaylistId}' could not be found");

            await _catalogRepository.DeletePlaylist(playlistDomain, cancellationToken);
            return Unit.Value;
        }

    }
}
