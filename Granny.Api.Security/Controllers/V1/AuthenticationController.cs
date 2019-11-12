using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Granny.Api.Security.Services;
using Granny.DataTransferObject;
using Granny.DataTransferObject.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Security.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _userService;
        private IMapper _mapper;

        public AuthenticationController(
            IAuthenticationService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto model)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(model.GoogleToken).ConfigureAwait(false);

                if (payload == null) return Unauthorized();

                var user = await _userService.Authenticate(payload.Email).ConfigureAwait(false);

                if (user == null) return BadRequest(new { message = "User unidentified" });

                UserOutputDto userDto = _mapper.Map<UserOutputDto>(user);
                return Ok(user);
            }
            catch (InvalidJwtException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}