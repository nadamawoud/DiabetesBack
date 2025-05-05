using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Services.Services
{
    namespace Diabetes.Services.Services
    {
        public class TokenClerk : ITokenService
        {
            private readonly IConfiguration _config;
            private readonly UserManager<AppUser> _userManager;

            public TokenClerk(IConfiguration config, UserManager<AppUser> userManager)
            {
                _config = config;
                _userManager = userManager;
            }

            public async Task<string> CreateToken(AppUser user)
            {
                try
                {
                    var tokenKey = _config["JwtSettings:TokenKey"] ??
                                   _config["TokenKey"] ??
                                   throw new Exception("JWT TokenKey is missing in configuration");

                    if (tokenKey.Length < 64)
                    {
                        throw new Exception("JWT TokenKey must be at least 64 characters long");
                    }

                    var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                    var roles = await _userManager.GetRolesAsync(user);
                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddDays(_config.GetValue("JwtSettings:ExpiryInDays", 7)),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return tokenHandler.WriteToken(token);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to generate token", ex);
                }
            }
        }
    }

}
