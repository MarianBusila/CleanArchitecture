using Catalog.Application.Playlists.Commands.CreatePlaylist;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using Catalog.Domain.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests.Playlists.Commands;

[Collection("Integration collection")]
public class CreatePlaylistTests  : BaseIntegrationTest
{
    public CreatePlaylistTests(Testing testing, ITestOutputHelper output) : base(testing, output)
    {
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
        var rockGenre = await _testing.AddAsync( new Genre { Name = "Rock" });
        var popGenre = await _testing.AddAsync( new Genre { Name = "Pop" });
        var mediaType = await _testing.AddAsync( new MediaType { Name = "MP4" });
        var artist = await _testing.AddAsync( new Artist { Name = "Artist1" });
        var album = await _testing.AddAsync( new Album { Title = "Album1", ArtistId = artist.Id});

        var track1 = await _testing.AddAsync(new Track { Name = "Song1", MediaTypeId = mediaType.Id, GenreId = rockGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(0.99) });
        var track2 = await _testing.AddAsync(new Track { Name = "Song2", MediaTypeId = mediaType.Id, GenreId = popGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(1.99) });
        
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
}