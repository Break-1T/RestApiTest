using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.v1.Models;
using AutoMapper;
using Context.Infrastructure;
using Microsoft.AspNetCore.Http;
using DbUser = Context.Models.User;

namespace Api.v1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        //public UserController() { }
        public UserController(ILogger<UserController> logger, IUserService service,IMapper mapper)
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
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<User>))]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            var contextUser = _mapper.Map<DbUser>(user);

            var result = await _service.AddUserAsync(contextUser, token);

            if (result == false)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }
            return this.CreatedAtAction("CreateUser", new { id = user.Id }, user);
            
        }
    }
}
