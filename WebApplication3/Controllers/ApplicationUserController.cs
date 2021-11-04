using Api.api.v1.Models;
using Api.Infrastructure.Attributes;
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

namespace Api.Controllers
{
    
    [ApiController]
    [V1,ApiRoute]
    public class ApplicationUserController : ControllerBase
    {

        private readonly ILogger<ApplicationUserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="emailService">The email service.</param>
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
        /// emailService
        /// or
        /// tokenService.
        /// </exception>
        public ApplicationUserController(
            ILogger<ApplicationUserController> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationUser> roleManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticateRequest user, CancellationToken cancellationToken)
        {
            try
            {
                var dbUser = this._mapper.Map<ApplicationUser>(user);

                var createUserResult = await this._userManager.CreateAsync(dbUser, user.Password);

                return this.Ok(this._mapper.Map<UserResponse>(dbUser));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
