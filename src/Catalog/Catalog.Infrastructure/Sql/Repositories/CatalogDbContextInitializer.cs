using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Sql.Repositories;

public static class InitializerExtensions
{
    public static void InitilizeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<CatalogDbContextInitializer>();
        initializer.Initialize();
        initializer.Seed();
    }
}

public class CatalogDbContextInitializer
{
    private readonly ILogger<CatalogDbContextInitializer> _logger;
    private readonly CatalogDbContext _catalogDbContext;
    
    public CatalogDbContextInitializer(ILogger<CatalogDbContextInitializer> logger, CatalogDbContext catalogDbContext)
    {
        _logger = logger;
        _catalogDbContext = catalogDbContext;
    }
    
    public void Initialize()
    {
        try
        {
            _catalogDbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public void Seed()
    {
        if (!_catalogDbContext.Playlists.Any())
        {
            _catalogDbContext.Playlists.AddRange(CreatePlaylists());
        }

        if (!_catalogDbContext.Tracks.Any())
        {
            _catalogDbContext.Tracks.AddRange(CreateTracks());
        }

        if (!_catalogDbContext.PlaylistTracks.Any())
        {
            _catalogDbContext.PlaylistTracks.AddRange(CreatePlaylistTracks());
        }

        _catalogDbContext.SaveChanges();
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