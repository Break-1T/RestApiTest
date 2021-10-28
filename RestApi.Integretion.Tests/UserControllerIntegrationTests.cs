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
using Api.api.v1.Models;
using Context.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Threading;
using AutoMapper;

namespace RestApi.Integration.Tests
{
    public class UserControllerIntegrationTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        public UserControllerIntegrationTests(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            client = factory.CreateClient();
        }

        private readonly TestWebApplicationFactory<Startup> _factory;
        private readonly HttpClient client;

        [Fact]
        public async Task GetUsersAsync_UsersList_ReturnOk()
        {
            // Arrange
            
            
            // Act
            
            var response = await client.GetAsync("/api/v2/User/list");

            var userApiModels = await response.Content.ReadFromJsonAsync<List<UserResponse>>();

            // Assert

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Assert.NotNull(userApiModels);
            Assert.NotEmpty(userApiModels);
        }
        [Fact]
        public async Task GetUsersAsync_IncorrectEndpoint_NotFound()
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync("User/error");
            
            List<UserResponse> userApiModels;
            try
            {
                userApiModels = await response.Content.ReadFromJsonAsync<List<UserResponse>>();
            }
            catch
            {
                userApiModels = null;
            }

            // Assert

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(userApiModels);
            
        }


        [Fact]
        public async Task GetUserAsync_UserReturn_Ok()
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync($"/api/v2/User/9");
            var user = await response.Content.ReadFromJsonAsync<UserResponse>();

            // Assert

            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(9,user.Id);
            Assert.Equal("userResponse", user.Name);
            Assert.Equal("userResponse", user.Surname);
            Assert.Equal(3, user.Age);
            Assert.Equal(null, user.Operations);

        }
        [Fact]
        public async Task GetUserAsync_InvalidId_ReturnBadRequest()
        {
            // Arrange
            

            // Act

            var response = await client.GetAsync($"api/v2/User/{999999}");
            UserResponse userResponse;
            try
            {
                userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            catch
            {
                userResponse = null;
            }

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(userResponse);
        }

        [Fact]
        public async Task CreateUserAsync_SuccessCreated_ReturnOk()
        {
            // Arrange
            
            var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "api/v1/User/create");
            var userApi = new UserResponse()
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

            var user = await response.Content.ReadFromJsonAsync<UserResponse>();

            Assert.Equal(userApi.Name, user?.Name);
            Assert.Equal(userApi.Surname, user?.Surname);
            Assert.Equal(userApi.Age, user?.Age);
            Assert.Equal(userApi.CurrentTime, user?.CurrentTime);
            Assert.Equal(userApi.Operations, user?.Operations);
        }

        [Fact]
        public async Task CreateUserAsync_Exception_userAge()
        {
            // Arrange
            var userApi = new UserResponse
            {
                Name = "One",
                Surname = "Two",
                Age = 400,
                CurrentTime = DateTime.Now,
                Operations = null
            };
            
            string jsonUser = JsonSerializer.Serialize(userApi);

            var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "api/v1/User/create");
            httpRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Act

            var response = await client.SendAsync(httpRequest);

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var s = await response.Content.ReadAsStringAsync();

        }
    }
}

