using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using Api.Models;
using Context;
using Context.Models;
using Context.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //public UserController() { }
        public UserController(ILogger<UserController> logger, IUserService service)
        {
            this._logger = logger;
            this._service = service;
            token = new CancellationToken();
        }

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly CancellationToken token;

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            //var l = new List<User>();
            //return this.Ok(l);

            var result = (await _service.GetUserAsync(token)).ToUserListApiModel();
            if (result == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }

            return this.Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var result = (await _service.GetUserAsync(id, token)).ToUserApiModel();
            if (result == null)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }
            return this.Ok(result);
        }

        [HttpDelete("delete/{id}", Name = "delete")]
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

        [HttpPost("create", Name = "create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> CreateUserAsync([FromBody]UserApiModel user)
        {
            var result = await _service.AddUserAsync(user.ToUser(),token);

            if (result == false)
            {
                _logger.LogWarning("DbException");
                return this.BadRequest("DbException");
            }

            if (ModelState.IsValid)
            {
                return this.CreatedAtAction("CreateUser", new { id = user.Id }, user);
            }

            return this.NotFound();
        }
    }
}
