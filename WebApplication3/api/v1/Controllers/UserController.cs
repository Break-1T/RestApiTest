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


namespace Api.api.v1.Controllers
{
    [ApiController]
    [V1,ApiRoute]
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
