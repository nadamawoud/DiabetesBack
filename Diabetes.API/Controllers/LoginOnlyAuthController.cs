
using Diabetes.Core.DTOs;
using Diabetes.Core.Entities;
using Diabetes.Services.Services;
using Diabetes.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginOnlyAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<LoginOnlyAuthController> _logger;

        public LoginOnlyAuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService,
            ILogger<LoginOnlyAuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                // التحقق من المدخلات
                if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                    return BadRequest("Email and password are required");

                // البحث عن المستخدم
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                    return Unauthorized("Invalid credentials");

                // التحقق من كلمة المرور
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Invalid credentials");

                // توليد التوكن
                var token = await _tokenService.GenerateToken(user);

                // الحصول على الدور
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                return Ok(new UserDto
                {
                    Email = user.Email,
                    Token = token,
                    Role = role,                    
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    
    }
}