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


namespace Api.api.v2.Controllers
{
    [ApiController]
    [V2,ApiRoute]
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

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            var contextUser = await _service.GetUserAsync(token);

            var apiUser = _mapper.Map<IEnumerable<User>>(contextUser);
            if (apiUser == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }

            return this.Ok(apiUser);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var contextUser = await _service.GetUserAsync(id, token);

            var apiUser = _mapper.Map<User>(contextUser);
            if (apiUser == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }
            return this.Ok(apiUser);
        }
    }
}
