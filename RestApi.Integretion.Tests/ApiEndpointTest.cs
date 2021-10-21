using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
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
using System.Threading;

namespace RestApi.Integration.Tests
{
    public class ApiEndpointTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        public ApiEndpointTest(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            client = factory.CreateClient();
        }

        private readonly TestWebApplicationFactory<Startup> _factory;
        private readonly HttpClient client;

        [Fact]
        public async Task GetUsersAsync_usersList_return_test()
        {
            // Arrange
            
            
            // Act
            
            var response = await client.GetAsync("User/list");

            var userApiModels = await response.Content.ReadFromJsonAsync<List<UserApiModel>>();

            // Assert

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Assert.NotNull(userApiModels);
            Assert.NotEmpty(userApiModels);
        }
        [Fact]
        public async Task GetUsersAsync_error_endpoint_return_Test()
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync("User/error");
            
            List<UserApiModel> userApiModels;
            try
            {
                userApiModels = await response.Content.ReadFromJsonAsync<List<UserApiModel>>();
            }
            catch
            {
                userApiModels = null;
            }

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(userApiModels);
            
        }


        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(9)]
        public async Task GetUserAsync_user_return_test(int id)
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync($"User/{id}");
            var user = await response.Content.ReadFromJsonAsync<UserApiModel>();

            // Assert

            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(id,user.Id);
        }
        [Fact]
        public async Task GetUserAsync_inaccessibleId_badRequest_Test()
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync($"User/{999999}");
            UserApiModel user;
            try
            {
                user = await response.Content.ReadFromJsonAsync<UserApiModel>();
            }
            catch
            {
                user = null;
            }

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(user);
        }

        [Fact]
        public async Task CreateUserAsync_httpRequestMessage_Test()
        {
            // Arrange
            
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
        public async Task CreateUserAsync_Exception_userAge_limit_400_Test()
        {
            // Arrange
            
            var userApi = new UserApiModel
            {
                Name = "One",
                Surname = "Two",
                Age = 120,
                CurrentTime = DateTime.Now,
                Operations = null
            };

            string jsonUser = JsonSerializer.Serialize(userApi);

            var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "User/create");
            httpRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Act

            var response = await client.SendAsync(httpRequest);

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

