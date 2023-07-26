using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Catalog.Application.Playlists.Commands.AddTracksToPlaylist
{
    public sealed class AddTracksToPlaylistCommandHandler : IRequestHandler<AddTracksToPlaylistCommand>
    {

        private readonly ICatalogRepository _catalogRepository;

        public AddTracksToPlaylistCommandHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        public async Task Handle(AddTracksToPlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Playlist playlistDomain = await _catalogRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            if (playlistDomain == null)
                throw new EntityNotFoundException($"A playlist having id '{request.PlaylistId}' could not be found");

            await _catalogRepository.AddTracksToPlaylist(playlistDomain, request.TrackIds, cancellationToken);
        }

    }
}
