using Catalog.Application.Tracks.Queries.GetTrack;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using Catalog.Domain.Models;
using FluentAssertions;
using FluentValidation;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests.Tracks.Queries;

[Collection("Integration collection")]
public class GetTrackListTests : BaseIntegrationTest
{
    public GetTrackListTests(Testing testing, ITestOutputHelper output) : base(testing, output)
    {
    }

    [Fact]
    public async Task ShouldGetTrackList()
    {
        var rockGenre = await _testing.AddAsync( new Genre { Name = "Rock" });
        var popGenre = await _testing.AddAsync( new Genre { Name = "Pop" });
        var mediaType = await _testing.AddAsync( new MediaType { Name = "MP4" });
        var artist = await _testing.AddAsync( new Artist { Name = "Artist1" });
        var album = await _testing.AddAsync( new Album { Title = "Album1", ArtistId = artist.Id});

        var track1 = await _testing.AddAsync(new Track { Name = "Song1", MediaTypeId = mediaType.Id, GenreId = rockGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(0.99) });
        var track2 = await _testing.AddAsync(new Track { Name = "Song2", MediaTypeId = mediaType.Id, GenreId = popGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(0.79) });
        var track3 = await _testing.AddAsync(new Track { Name = "Song3", MediaTypeId = mediaType.Id, GenreId = popGenre.Id, AlbumId = album.Id, UnitPrice = new decimal(1.99) });

        var playlist = await _testing.AddAsync(new Playlist {Name = "Music 90s" });
        
        await _testing.AddAsync(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track1.Id });
        await _testing.AddAsync(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track2.Id });
        await _testing.AddAsync(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track3.Id });
        
        var query = new GetTrackListQuery(new TrackQuery
        {
            PriceFrom = "gte:1.2"
        });
        
        IPagedCollection<TrackDetail> result = await _testing.SendAsync(query);
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var trackDetail = result.ElementAt(0);
        trackDetail.Name.Should().Be("Song3");
    }

    [Fact]
    public async Task ShouldRequireValidPriceFromFilter()
    {
        var query = new GetTrackListQuery(new TrackQuery
        {
            PriceFrom = "invalid"
        });

        await FluentActions.Invoking(() => 
            _testing.SendAsync(query)).Should().ThrowAsync<ValidationException>();

    }
}