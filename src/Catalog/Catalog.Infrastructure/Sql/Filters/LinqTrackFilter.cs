using System;
using System.Linq;
using System.Linq.Expressions;
using Catalog.Application.Tracks.Queries.GetTrack.Filters;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Filters
{
    public sealed class LinqTrackFilter : ITrackFilter
    {
        public Expression<Func<Track, bool>> Filter { get; private set; } = ExpressionBuilder.True<Track>();

        public ITrackFilter WhereAlbumLike(string album)
        {
            if (!string.IsNullOrWhiteSpace(album))
                Filter = Filter.And(e => EF.Functions.ILike(e.Album!.Title, $"%{album}%"));

            return this;
        }

        public ITrackFilter WhereArtistLike(string artist)
        {
            if (!string.IsNullOrWhiteSpace(artist))
                Filter = Filter.And(e => EF.Functions.ILike(e.Album!.Artist!.Name, $"%{artist}%"));

            return this;
        }

        public ITrackFilter WhereComposerLike(string composer)
        {
            if (!string.IsNullOrWhiteSpace(composer))
                Filter = Filter.And(e => EF.Functions.ILike(e.Composer, $"%{composer}%"));

            return this;
        }

        public ITrackFilter WhereGenreLike(string genre)
        {
            if (!string.IsNullOrWhiteSpace(genre))
                Filter = Filter.And(e => EF.Functions.ILike(e.Genre!.Name, $"%{genre}%"));

            return this;
        }

        public ITrackFilter WhereMediaTypeLike(string mediaType)
        {
            if (!string.IsNullOrWhiteSpace(mediaType))
                Filter = Filter.And(e => EF.Functions.ILike(e.MediaType!.Name, $"%{mediaType}%"));

            return this;
        }

        public ITrackFilter WhereNameLike(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Filter = Filter.And(e => EF.Functions.ILike(e.Name, $"%{name}%"));

            return this;
        }

        public ITrackFilter WherePlaylistIdEquals(int? playlistId)
        {
            if (playlistId > 0)
                Filter = Filter.And(track => track.PlaylistTracks.Select(pt => pt.PlaylistId).Contains(playlistId.Value));

            return this;
        }

        public ITrackFilter WherePrice(string fromPrice, string toPrice)
        {
            if (fromPrice != null)
                Filter = Filter.And(new LinqPriceFilter(fromPrice).Expression());

            if (toPrice != null)
                Filter = Filter.And(new LinqPriceFilter(toPrice).Expression());

            return this;
        }

    }
}
