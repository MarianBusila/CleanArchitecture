using Catalog.Application.Playlists.Queries.GetPlaylist;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Domain.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests;

[Collection("Integration collection")]
public class GetPlaylistListTests : IAsyncLifetime
{
    private readonly Testing _testing;

    public GetPlaylistListTests(Testing testing, ITestOutputHelper output)
    {
        _testing = testing;
        _testing.SetOutput(output);
    }
    
    [Fact]
    public async Task ShouldReturnAllPlaylistsWithTracks()
    {
        var rockGenre = await _testing.AddAsync( new Genre { Name = "Rock" });
        var popGenre = await _testing.AddAsync( new Genre { Name = "Pop" });
        var mediaType = await _testing.AddAsync( new MediaType { Name = "MP4" });
        var artist = await _testing.AddAsync( new Artist { Name = "Artist1" });
        var album = await _testing.AddAsync( new Album { Title = "Album1", ArtistId = artist.Id});

        var track1 = await _testing.AddAsync(new Track { Name = "Song1", MediaTypeId = mediaType.Id, GenreId = rockGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(0.99) });
        var track2 = await _testing.AddAsync(new Track { Name = "Song2", MediaTypeId = mediaType.Id, GenreId = popGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(1.99) });

        var playlist = await _testing.AddAsync(new Playlist {Name = "Music 90s" });
        
        await _testing.AddAsync(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track1.Id });
        await _testing.AddAsync(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track2.Id });
        
        var query = new GetPlaylistListQuery(new PlaylistQuery());

        // Act
        IPagedCollection<PlaylistDetail> result = await _testing.SendAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.First().TrackCount.Should().Be(2);
    }
    
    [Fact]
    public async Task ShouldReturnAllPlaylists()
    {
        // Arrange
        await _testing.AddAsync(new Playlist
        {
            Name = "MJ"
        });
        
        await _testing.AddAsync(new Playlist
        {
            Name = "ABBA"
        });
        
        var query = new GetPlaylistListQuery(new PlaylistQuery());

        // Act
        IPagedCollection<PlaylistDetail> result = await _testing.SendAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }

    public async Task InitializeAsync()
    {
        await _testing.ResetState();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}