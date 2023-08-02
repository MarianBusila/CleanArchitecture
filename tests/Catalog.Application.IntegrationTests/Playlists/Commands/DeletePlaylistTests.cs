using Catalog.Application.Playlists.Commands.DeletePlaylist;
using Catalog.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests.Playlists.Commands;

[Collection("Integration collection")]
public class DeletePlaylistTests : BaseIntegrationTest
{
    public DeletePlaylistTests(Testing testing, ITestOutputHelper output) : base(testing, output)
    {
    }

    [Fact]
    public async Task ShouldRequireValidPlaylistId()
    {
        var command = new DeletePlaylistCommand(100);

        await FluentActions.Invoking(() => 
            _testing.SendAsync(command)).Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task ShouldDeletePlaylist()
    {
        var playlist = await _testing.AddAsync(new Playlist {Name = "Music 90s" });
        
        var command = new DeletePlaylistCommand(playlist.Id);

        await _testing.SendAsync(command);

        var playlistDeleted = await _testing.FindAsync<Playlist>(playlist.Id);

        playlistDeleted.Should().BeNull();
    }
}