using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Granny.DataModel;
using Granny.Repository.Security;
using Granny.Util.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Granny.Api.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly AppSettings _appSettings;

        private readonly IUserRepository _securityRepository;

        public AuthenticationService(IOptions<AppSettings> appSettings, IUserRepository securityRepository)
        {
            _appSettings = appSettings.Value;
            _securityRepository = securityRepository;
        }

        public async Task<AuthenticatedUser> Authenticate(string email)
        {
            var user = await _securityRepository.GetUser(email).ConfigureAwait(false);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticatedUser
            {
                Email = user.Email,
                Name = user.Name,   
                Token = tokenHandler.WriteToken(token),
                UserId = user.UserId
            };
        }
    }
}
