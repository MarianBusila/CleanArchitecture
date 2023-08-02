using Catalog.Application.Playlists.Queries.GetPlaylist;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Domain.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests;

public class GetPlaylistListTests : IClassFixture<Testing>, IAsyncLifetime
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
        await _testing.AddAsync( new Genre { Id = 1, Name = "Rock" });
        await _testing.AddAsync( new Genre { Id = 2, Name = "Pop" });
        await _testing.AddAsync( new MediaType { Id = 1, Name = "MP4" });
        await _testing.AddAsync( new Artist { Id = 1, Name = "Artist1" });
        await _testing.AddAsync( new Album { Id = 1, Title = "Album1", ArtistId = 1});

        var track1 = await _testing.AddAsync(new Track { Name = "Song1", MediaTypeId = 1, GenreId = 1, AlbumId = 1, UnitPrice = new decimal(0.99) });
        var track2 = await _testing.AddAsync(new Track { Name = "Song2", MediaTypeId = 1, GenreId = 2, AlbumId = 1, UnitPrice = new decimal(1.99) });

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