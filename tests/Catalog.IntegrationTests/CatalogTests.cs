using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.Api;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Catalog.IntegrationTests
{
    public class CatalogTests : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {

        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public CatalogTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _factory.Output = output;
            _client = _factory.CreateClient();
        }

        public void Dispose() => _factory.Output = null;

        [Fact]
        public async Task GetPlaylists_should_return_ok()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("v1/playlists");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().Contain(header => header.Key == "X-Pagination");
            string json = await response.Content.ReadAsStringAsync();
            var playlists = JsonConvert.DeserializeObject<IReadOnlyCollection<PlaylistDetail>>(json);
            playlists.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetTracks_should_return_ok()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("v1/tracks?price-from=gte:1.2");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().Contain(header => header.Key == "X-Pagination");
            string json = await response.Content.ReadAsStringAsync();
            var tracks = JsonConvert.DeserializeObject<IReadOnlyCollection<TrackDetail>>(json);
            tracks.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetTracks_should_return_bad_request_for_invalid_filter()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("v1/tracks?price-from=abc:1.2");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            string json = await response.Content.ReadAsStringAsync();
            var problem = JsonConvert.DeserializeObject<ValidationProblemDetails>(json);
            problem.Should().NotBeNull();
            problem.Errors.Should().ContainKey("PriceFrom");
        }
    }
}
