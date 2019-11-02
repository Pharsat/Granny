﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Granny.Api.Security.Helpers;
using Granny.DataModel;
using Granny.Repository.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Granny.Api.Security.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;

        private readonly ISecurityRepository _securityRepository;

        public UserService(IOptions<AppSettings> appSettings)
        {
            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            _appSettings = appSettings.Value;
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
                    new Claim(ClaimTypes.Name, email)
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