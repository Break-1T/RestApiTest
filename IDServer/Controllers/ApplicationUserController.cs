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
using Context;

namespace IDServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RestApiContext _dbContext;

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
        public ApplicationUserController(UserManager<ApplicationUser> userManager, IMapper mapper, RestApiContext dbContext)
        {
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._dbContext = dbContext;
        }
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticateRequest user, CancellationToken cancellationToken=default)
        {

            try
            {
                var random = new Random().Next(0, 99999);
                var idenityUser = new ApplicationUser()
                {
                    Id = random.ToString(),
                    UserName = user.Username,
                    NormalizedUserName = new UpperInvariantLookupNormalizer().NormalizeName(user.Username),
                    Password = user.Password,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, user.Password),
                    EmailConfirmed = true,
                    Email = "email",
                    NormalizedEmail = "EMAIL",
                    ConcurrencyStamp = " ",
                    SecurityStamp = " ",
                };
                var createUserResult = await this._userManager.CreateAsync(idenityUser);

                return this.Ok(idenityUser);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("temp")]
        public async Task<IActionResult> AddTemp()
        {
            this._dbContext.Temps.Add(new Temp());
            await this._dbContext.SaveChangesAsync();
            return this.Ok();
        }
    }
}
