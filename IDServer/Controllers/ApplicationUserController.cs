using AutoMapper;
using Context.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using IDServer.Models;

namespace IDServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="tokenService">The token service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// userManager
        /// or
        /// roleManager
        /// or
        /// mapper
        /// or
        /// tokenService
        /// </exception>
        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user, CancellationToken cancellationToken=default)
        {
            try
            {
                var createUserResult = await this._userManager.CreateAsync(user, user.Password);

                return this.Ok(user);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
