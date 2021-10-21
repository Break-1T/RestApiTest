using System;
using System.Threading.Tasks;
using Api;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RestApi.Integration.Tests
{
    public class ApiEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public ApiEndpointTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private readonly WebApplicationFactory<Startup> _factory;

        [Theory]
        [InlineData("")]
        public async Task Get(string url)
        {
            // Arrange

            var client = _factory.CreateClient();

            // Act

            var response = await client.GetAsync(url);

            // Assert

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
