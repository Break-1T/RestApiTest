using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Context;
using Context.Models;
using Context.Infrastructure;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController() { }
        public UserController(ILogger<UserController> logger, IUserService service)
        {
            this._logger = logger;
            this._service = service;
            token = new CancellationToken();
        }

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly CancellationToken token;

        [HttpGet]
        public virtual async Task<IEnumerable<User>> GetAsync()
        {
            //var l = new List<User>();
            //return this.Ok(l);
            try
            {
                var result = await _service.GetUserAsync(token);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,e,e.Message);
                return null;
            }

        }
        [HttpGet("{id}",Name="GetAsync")]
        public virtual async Task<User> GetAsync(int id)
        {
            //var l = new List<User>();
            //return this.Ok(l);
            try
            {
                var result = await _service.GetUserAsync(id, token);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, e.Message);
                return null;
            }

        }

        [HttpDelete("{id}",Name = "DeleteAsync")]
        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _service.DeleteUserAsync(id, token);
                if (result==false)
                    throw new Exception();
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, e.Message);
                return false;
            }
        }

        [HttpPost]
        public virtual async Task<bool> PostAsync()
        {
            try
            {
                var result = await _service.AddUserAsync(token);
                if (result == false)
                    throw new Exception();
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, e.Message);
                return false;
            }
        }
    }
}
