using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Granny.Api.Security.Services;
using Granny.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Security.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateDto model)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(model.GoogleToken).ConfigureAwait(false);
                if (payload == null) return Unauthorized();

                var user = await _userService.Authenticate(payload.Email).ConfigureAwait(false);

                if (user == null)
                    return BadRequest(new { message = "email is invalid" });

                return Ok(user);
            }
            catch(InvalidJwtException ex)
            {
                return Unauthorized(new { message = ex.Message});
            }
        }
    }
}
