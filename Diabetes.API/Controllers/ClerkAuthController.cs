using Diabetes.Core.DTOs;
using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class ClerkAuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public ClerkAuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("clerklogin")]
        public async Task<ActionResult<ClerkLoginResponseDto>> ClerkLogin([FromBody] ClerkLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid Email or Password");

            if (user.Clerk == null)
                return Unauthorized("User is not a Clerk");

            if (!user.EmailConfirmed)
                return Unauthorized("Email is not confirmed yet");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid Email or Password");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
                return Unauthorized("No role assigned to this user");

            var token = await _tokenService.CreateToken(user);

            return Ok(new ClerkLoginResponseDto
            {
                Token = token,
                Role = role
            });
        }
    }
}

