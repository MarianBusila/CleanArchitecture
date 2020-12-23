using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using Common.Microsoft.EntityFrameworkCore;
using MediatR;

namespace Catalog.Application.Playlists.Commands.DeleteTracksFromPlaylist
{
    public sealed class DeleteTracksFromPlaylistCommandHandler : IRequestHandler<DeleteTracksFromPlaylistCommand>
    {

        private readonly ICatalogRepository _catalogRepository;

        public DeleteTracksFromPlaylistCommandHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        public async Task<Unit> Handle(DeleteTracksFromPlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Playlist playlistDomain = await _catalogRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            if (playlistDomain == null)
                throw new EntityNotFoundException($"A playlist having id '{request.PlaylistId}' could not be found");

            await _catalogRepository.DeleteTracksFromPlaylist(playlistDomain, request.TrackIds, cancellationToken);

            return Unit.Value;
        }

    }
}
