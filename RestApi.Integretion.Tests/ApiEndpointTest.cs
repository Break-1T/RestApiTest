using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Api;
using Api.Infrastructure.Extensions;
using Api.Models;
using Context.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace RestApi.Integration.Tests
{
    public class ApiEndpointTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        public ApiEndpointTest(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private readonly TestWebApplicationFactory<Startup> _factory;

        [Fact]
        public async Task GetUsersAsync__()
        {
            // Arrange
            
            var client = _factory.CreateClient();
            
            // Act
            
            var response = await client.GetAsync("User/list");
            
            // Assert

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }
        [Fact]
        public async Task GetUsersAsync_error_endpoint_return_Test()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("User/error");
            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(9)]
        public async Task GetUserAsync_Test(int id)
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync($"User/{id}");
            // Assert

            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetUserAsync_Exception_Test()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync($"User/{100}");
            // Assert

            //response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_Test()
        {
            // Arrange

            var client = _factory.CreateClient();

            var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "User/create");
            var userApi = new UserApiModel()
            {
                Name = "One",
                Surname = "Two",
                Age = 25,
                CurrentTime = DateTime.Now,
                Operations = null
            };
            string jsonUser = JsonSerializer.Serialize(userApi);
            httpRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
           
            // Act

            var response = await client.SendAsync(httpRequest);

            // Assert

            response.EnsureSuccessStatusCode(); //200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_Exception_Test()
        {
            // Arrange

            var client = _factory.CreateClient();
            var userApi = new UserApiModel()
            {
                Name = "One",
                Surname = "Two",
                Age = 25,
                CurrentTime = DateTime.Now,
                Operations = null
            };
            string jsonUser = JsonSerializer.Serialize(userApi);

            var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "User/create");
            httpRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Act



            var response = await client.SendAsync(httpRequest);

            // Assert

            //response.EnsureSuccessStatusCode(); //200-299
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

