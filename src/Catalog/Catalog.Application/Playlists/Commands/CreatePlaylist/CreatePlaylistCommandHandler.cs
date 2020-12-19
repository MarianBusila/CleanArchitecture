using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using MediatR;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist
{
    public sealed class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, PlaylistFromCreate>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CreatePlaylistCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlaylistFromCreate> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var playlist = _mapper.Map<Playlist>(request);
            await _catalogRepository.CreatePlaylist(playlist, request.TrackIds, cancellationToken);
            return _mapper.Map<PlaylistFromCreate>(playlist);
        }

    }
}
