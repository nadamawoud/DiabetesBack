using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Diabetes.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Diabetes.Services.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(AppUser user)
        {
            try
            {
                // احصل على المفتاح وتأكد من طوله
                var tokenKey = _config["JwtSettings:TokenKey"] ??
                             _config["TokenKey"] ??
                             throw new Exception("JWT TokenKey is missing in configuration");

                // تحقق من طول المفتاح (64 حرف على الأقل)
                if (tokenKey.Length < 64)
                {
                    throw new Exception("JWT TokenKey must be at least 64 characters long");
                }

                // إنشاء claims
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                // إضافة الأدوار
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                // إنشاء المفتاح
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

                // استخدام خوارزمية HmacSha256 بدلاً من HmacSha512
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                // وصف Token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(_config.GetValue("JwtSettings:ExpiryInDays", 7)),
                    SigningCredentials = creds
                };

                // توليد Token
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