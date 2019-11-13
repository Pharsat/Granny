using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Granny.DataModel;
using Granny.DataTransferObject.User;
using Granny.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Security.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]  
    public class UsersController : ControllerBase
    {
        private IUserServices _userService;
        private IMapper _mapper;

        public UsersController(
            IUserServices userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> Post(
            [FromBody] UserCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.GoogleToken).ConfigureAwait(false);

            if (payload == null) return Unauthorized();

            if (await _userService.GetUser(payload.Email).ConfigureAwait(false) != null) { return Conflict(new { message = "User already exists" }); }

            User user = _mapper.Map<User>(payload);

            return Ok(await _userService.CreateUser(user).ConfigureAwait(false));
        }

        [HttpGet("{email}", Name = "GetByEmail")]
        public async Task<IActionResult> GetByEmail(
            [EmailAddress] string email)
        {
            if (!ModelState.IsValid) return BadRequest();
            User user = await _userService.GetUser(email).ConfigureAwait(false);
            UserOutputDto userDto = _mapper.Map<UserOutputDto>(user);
            return Ok(userDto);
        }
    }
}
