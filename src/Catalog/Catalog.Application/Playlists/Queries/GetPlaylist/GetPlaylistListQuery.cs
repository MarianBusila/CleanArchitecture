using System.Collections.Generic;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using MediatR;

namespace Catalog.Application.Playlists.Queries.GetPlaylist
{
    public sealed class GetPlaylistListQuery : IRequest<IEnumerable<PlaylistDetail>>
    {
        public string Name { get; }
        public string Order { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int? TrackId { get; }

        public GetPlaylistListQuery(PlaylistQuery playlistQuery)
        {
            if (playlistQuery is null)
                throw new System.ArgumentNullException(nameof(playlistQuery));

            Name = playlistQuery.Name;
            Order = playlistQuery.Order;
            PageNumber = playlistQuery.PageNumber;
            PageSize = playlistQuery.PageSize;

        }
        public GetPlaylistListQuery(int trackId, PlaylistQuery playlistQuery) : this(playlistQuery)
        {
            TrackId = trackId;
        }
    }
}
