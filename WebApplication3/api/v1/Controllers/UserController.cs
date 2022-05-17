using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.api.v1.Models;
using Api.Infrastructure.Attributes;
using AutoMapper;
using Context.Infrastructure;
using Microsoft.AspNetCore.Http;
using DbUser = Context.Models.User;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Context.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System;
using System.Text.Json;

namespace Api.api.v1.Controllers
{
    [ApiController]
    [V1,ApiRoute]
    public class UserController : ControllerBase
    {
        //public UserController() { }
        public UserController(ILogger<UserController> logger, IUserService service,IMapper mapper, IConfiguration configuration, IHttpClientFactory httpClient)
        {
            this._logger = logger;
            this._service = service;
            this.token = new CancellationToken();
            this._mapper = mapper;
            this._configuration = configuration;
            this._httpClient = httpClient.CreateClient();
        }

        private const string ApiRoute = "connect/token";

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly CancellationToken token;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        [Authorize]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteUserAsync(id, token);
            if (result == false)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
                
            }
            return this.Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> CreateUserAsync([FromBody] UserResponse userResponse)
        {
            var contextUser = _mapper.Map<DbUser>(userResponse);

            var result = await _service.AddUserAsync(contextUser, token);

            if (result == false)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }
            return this.CreatedAtAction("CreateUser", new { id = userResponse.Id }, userResponse);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery]LoginRequest model)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "client_id", "MyClient" },
                        { "client_secret", "secret" },
                        { "grant_type", "password" },
                        { "scope", "User" },
                        { "username", model.Username },
                        { "password", model.Password },
                    }),
                RequestUri = new Uri(new Uri(this._configuration.GetSection("IdentityServer:Url").Value), ApiRoute),
            };

            var result = await this._httpClient.SendAsync(requestMessage);
            var type = new
            {
                access_token = "",
                expires_in = 0,
                token_type = "",
                scope = "",
            };

            return this.Ok(JsonSerializer.Deserialize(await result.Content?.ReadAsStringAsync(), type.GetType()));
        }
    }
}
