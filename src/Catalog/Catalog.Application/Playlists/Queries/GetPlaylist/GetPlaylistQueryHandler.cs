using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Application.Repositories;
using Catalog.Domain.Models;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistQueryHandler : IRequestHandler<GetPlaylistQuery, PlaylistDetail>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetPlaylistQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<PlaylistDetail> Handle(GetPlaylistQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));


            Playlist playlistDomain = await _catalogRepository.GetPlaylist(request.PlaylistId, cancellationToken);
            return playlistDomain == null ? null : _mapper.Map<PlaylistDetail>(playlistDomain);
        }

    }
}