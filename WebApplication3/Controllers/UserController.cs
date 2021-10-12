using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            this._service = service;
        }

        [HttpGet]
        public IActionResult<IEnumerable<User>> Get()
        {

            //var l = new List<User>();
            //return this.Ok(l);
            return this.Get().ExecuteResultAsync(()=> _service.GetUser());
        }

        [HttpDelete]
        public IEnumerable<User> Delete()
        {
            return new List<User>();
        }
        [HttpPost]
        public void Post()
        {
            /*UserService.AddUser(new User(){Age = 19, Name = "Taras", Surname = "Ivanov"})*/
        }
    }
}
