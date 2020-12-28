using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Catalog.IntegrationTests
{
    public class CatalogTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public CatalogTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        [Fact]
        public async Task GetPlaylists_should_return_ok()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("v1/playlists");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
