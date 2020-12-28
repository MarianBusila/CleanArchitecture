using System.Collections.Generic;
using Catalog.Domain.Models;
using Catalog.Infrastructure.Sql.Repositories;

namespace Catalog.IntegrationTests
{
    public static class CatalogSeed
    {

        public static void InitializeDbForTests(CatalogDbContext catalogDbContext)
        {
            catalogDbContext.Playlists.RemoveRange(catalogDbContext.Playlists);
            catalogDbContext.Playlists.AddRange(CreatePlaylists());

            catalogDbContext.Tracks.RemoveRange(catalogDbContext.Tracks);
            catalogDbContext.Tracks.AddRange(CreateTracks());

            catalogDbContext.PlaylistTracks.RemoveRange(catalogDbContext.PlaylistTracks);
            catalogDbContext.PlaylistTracks.AddRange(CreatePlaylistTracks());

            catalogDbContext.SaveChanges();
        }

        private static IEnumerable<Playlist> CreatePlaylists()
        {
            return new List<Playlist>
            {
                new Playlist {Id = 1, Name = "Music 90s"},
                new Playlist {Id = 2, Name = "Music Rock"},
                new Playlist {Id = 3, Name = "Music Pop"}
            };
        }

        private static IEnumerable<Track> CreateTracks()
        {
            var genreRock = new Genre { Id = 1, Name = "Rock" };
            var genrePop = new Genre { Id = 2, Name = "Pop" };
            var mediaTypeMP4 = new MediaType { Id = 1, Name = "MP4" };
            var artist = new Artist { Id = 1, Name = "Artist1" };
            var album = new Album { Id = 1, Title = "Album1", Artist = artist};

            return new List<Track>
            {
                new Track {Id = 1, Name = "Song1", MediaType = mediaTypeMP4, Genre = genrePop, Album = album, UnitPrice = new decimal(0.99)},
                new Track {Id = 2, Name = "Song2", MediaType = mediaTypeMP4, Genre = genrePop, Album = album, UnitPrice = new decimal(1.99)},
                new Track {Id = 3, Name = "Song3", MediaType = mediaTypeMP4, Genre = genreRock, Album = album, UnitPrice = new decimal(1.49)},
                new Track {Id = 4, Name = "Song4", MediaType = mediaTypeMP4, Genre = genreRock, Album = album, UnitPrice = new decimal(1.89)},
                new Track {Id = 5, Name = "Song5", MediaType = mediaTypeMP4, Genre = genreRock, Album = album, UnitPrice = new decimal(0.99)}

            };
        }

        private static IEnumerable<PlaylistTrack> CreatePlaylistTracks()
        {
            return new List<PlaylistTrack>
            {
                new PlaylistTrack {PlaylistId = 1, TrackId = 1},
                new PlaylistTrack {PlaylistId = 1, TrackId = 2},
                new PlaylistTrack {PlaylistId = 1, TrackId = 3},
                new PlaylistTrack {PlaylistId = 2, TrackId = 4},
                new PlaylistTrack {PlaylistId = 2, TrackId = 5}

            };
        }

    }
}
