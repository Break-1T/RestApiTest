using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.api.v1.Models;
using Api.Infrastructure.Attributes;
using AutoMapper;
using Context.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Api.api.v2.Controllers
{
    [ApiController]
    [V2,ApiRoute]
    [Authorize]
    public class UserController : Controller
    {
        public UserController(ILogger<UserController> logger, IUserService service, IMapper mapper)
        {
            this._logger = logger;
            this._service = service;
            this.token = new CancellationToken();
            this._mapper = mapper;
        }

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly CancellationToken token;
        private readonly IMapper _mapper;

        [Authorize]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<List<UserResponse>>> GetUsersAsync()
        {
            var contextUser = await _service.GetUserAsync(token);

            var apiUser = _mapper.Map<IEnumerable<UserResponse>>(contextUser);
            if (apiUser == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }

            return this.Ok(apiUser);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<UserResponse>> GetUserAsync(int id)
        {
            var contextUser = await _service.GetUserAsync(id, token);

            var apiUser = _mapper.Map<UserResponse>(contextUser);
            if (apiUser == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }
            return this.Ok(apiUser);
        }
    }
}
