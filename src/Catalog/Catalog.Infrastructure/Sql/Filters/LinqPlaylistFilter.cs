using System;
using System.Linq;
using System.Linq.Expressions;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Filters
{
    public sealed class LinqPlaylistFilter : IPlaylistFilter
    {

        public Expression<Func<Playlist, bool>> Filter { get; private set; } = ExpressionBuilder.True<Playlist>();

        public IPlaylistFilter WhereNameLike(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Filter = Filter.And(e => EF.Functions.ILike(e.Name, $"%{name}%"));

            return this;
        }

        public IPlaylistFilter WhereTrackIdEquals(int? trackId)
        {
            if (trackId.HasValue)
                Filter = Filter.And(playlist => playlist.PlaylistTracks.Select(playlistTrack => playlistTrack.TrackId).Contains(trackId.Value));

            return this;
        }

    }
}
