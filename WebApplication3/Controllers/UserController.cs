using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Contex;
using Contex.Models;
using Contex.Infrastructure;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly CancellationToken token;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            this._service = service;
            token = new CancellationToken();
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {

            //var l = new List<User>();
            //return this.Ok(l);
            var result = await _service.GetUserAsync(token);
            if (result.Any())
            {
                _logger.LogWarning("Success");
            }
            else
            {
                _logger.LogWarning("Error");
            }
            return result;
        }
        [HttpGet("{id}",Name="GetAsync")]
        public async Task<User> GetAsync(int id)
        {

            //var l = new List<User>();
            //return this.Ok(l);
            var result = await _service.GetUserAsync(id, token);
            if (result.Id!=0)
            {
                _logger.LogWarning("Success");
            }
            else
            {
                _logger.LogWarning("Error");
            }
            return result;
        }

        [HttpDelete("{id}",Name = "DeleteAsync")]
        public async void DeleteAsync(int id)
        {
            if (await _service.DeleteUserAsync(id, token))
            {
                _logger.LogWarning("Success");
            }
        }

        [HttpPost]
        public async void PostAsync()
        {
            if (await _service.AddUserAsync(token))
            {
                _logger.LogWarning("Success");

            }
        }
       
    }
}
