using Api.api.v1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.api.v2
{
    public class TokenController : Controller
    {
        //private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        /// <exception cref="System.ArgumentNullException">tokenService.</exception>
        //public TokenController(ITokenService tokenService)
        //{
        //    this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        //}

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Action result.</returns>
        //[HttpPost]
        //[Route("create")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(SerializableError), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
        ////[ProducesResponseType(typeof(AuthToken), StatusCodes.Status201Created)]
        //public async Task<IActionResult> CreateToken([FromBody] AuthenticateRequest userRequest, CancellationToken cancellationToken = default)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState.ToParamSerializableErrors(typeof(UserLoginRequest)));
        //    }

        //    var createTokenResult = await this._tokenService.CreateAsync(userRequest, cancellationToken);

        //    if (createTokenResult.IsError)
        //    {
        //        return this.BadRequest(createTokenResult.Errors);
        //    }

        //    return this.Created(this.Request.Path.ToString(), createTokenResult.AuthToken);
        //}
    }
}
