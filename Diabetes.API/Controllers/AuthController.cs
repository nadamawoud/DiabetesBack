using Diabetes.Core.DTOs;
using Diabetes.Services;
using Diabetes.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Diabetes.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("clerk/register")]
        public async Task<IActionResult> RegisterClerk([FromBody] RegisterClerkDto registerDto)
        {
            var result = await _authService.RegisterClerkAsync(registerDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Registration successful. Please check your email for verification code." });
        }

        [HttpPost("clerk/verify")]
        public async Task<IActionResult> VerifyClerkEmail([FromBody] VerifyEmailDto verifyDto)
        {
            var isVerified = await _authService.VerifyClerkEmailAsync(verifyDto);

            if (!isVerified)
            {
                return BadRequest("Invalid email or verification code.");
            }

            return Ok(new { Message = "Email verified successfully." });
        }

        [HttpPost("casual/register")]
        public async Task<IActionResult> RegisterCasualUser([FromBody] RegisterCasualUserDto registerDto)
        {
            var result = await _authService.RegisterCasualUserAsync(registerDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Registration successful. Please check your email for verification code." });
        }

        [HttpPost("casual/verify")]
        public async Task<IActionResult> VerifyCasualUserEmail([FromBody] VerifyEmailDto verifyDto)
        {
            var isVerified = await _authService.VerifyCasualUserEmailAsync(verifyDto);

            if (!isVerified)
            {
                return BadRequest("Invalid email or verification code.");
            }

            return Ok(new { Message = "Email verified successfully." });
        }

        [HttpPost("doctor/register")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto registerDto)
        {
            var result = await _authService.RegisterDoctorAsync(registerDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Doctor registration submitted. Your account will be activated after approval by Medical Syndicate." });
        }
    }
}

