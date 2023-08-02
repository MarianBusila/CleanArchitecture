using Catalog.Application.Playlists.Commands.CreatePlaylist;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using Catalog.Domain.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests.Playlists.Commands;

public class CreatePlaylistTests  : IClassFixture<Testing>, IAsyncLifetime
{
    private readonly Testing _testing;
    
    public CreatePlaylistTests(Testing testing, ITestOutputHelper output)
    {
        _testing = testing;
        _testing.SetOutput(output);
    }

    [Fact]
    public async Task ShouldCreatePlaylistNoTracks()
    {
        var command = new CreatePlaylistCommand(new PlaylistForCreate
        {
            Name = "Music 80s"
        });

        var playlistCreated =  await _testing.SendAsync(command);
        var playlist = await _testing.FindAsync<Playlist>(playlistCreated.Id);

        playlist.Should().NotBeNull();
        playlist!.Name.Should().Be(command.Name);
    }
    
    [Fact]
    public async Task ShouldCreatePlaylistWithTracks()
    {
        await _testing.AddAsync( new Genre { Id = 1, Name = "Rock" });
        await _testing.AddAsync( new Genre { Id = 2, Name = "Pop" });
        await _testing.AddAsync( new MediaType { Id = 1, Name = "MP4" });
        await _testing.AddAsync( new Artist { Id = 1, Name = "Artist1" });
        await _testing.AddAsync( new Album { Id = 1, Title = "Album1", ArtistId = 1});

        var track1 = await _testing.AddAsync(new Track { Name = "Song1", MediaTypeId = 1, GenreId = 1, AlbumId = 1, UnitPrice = new decimal(0.99) });
        var track2 = await _testing.AddAsync(new Track { Name = "Song2", MediaTypeId = 1, GenreId = 2, AlbumId = 1, UnitPrice = new decimal(1.99) });
        
        var command = new CreatePlaylistCommand(new PlaylistForCreate
        {
            Name = "Music 90s",
            TrackIds = new []{track1.Id, track2.Id}
        });

        var playlistCreated =  await _testing.SendAsync(command);
        var playlist = await _testing.FindAsync<Playlist>(playlistCreated.Id);

        playlist.Should().NotBeNull();
        playlist!.Name.Should().Be(command.Name);
        //playlistTracks.Should().HaveCount(2);
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