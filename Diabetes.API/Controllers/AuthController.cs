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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("clerk/register")]
        public async Task<IActionResult> RegisterClerk([FromBody] RegisterClerkDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterClerkAsync(registerDto);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register clerk: {Errors}", result.Errors);
                    return BadRequest(result.Errors);
                }

                return Ok(new { Message = "Clerk registration successful. Please check your email for verification code." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering clerk");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("clerk/verify")]
        public async Task<IActionResult> VerifyClerkEmail([FromBody] VerifyEmailDto verifyDto)
        {
            try
            {
                var isVerified = await _authService.VerifyClerkEmailAsync(verifyDto);

                if (!isVerified)
                {
                    _logger.LogWarning("Failed to verify clerk email: {Email}", verifyDto.Email);
                    return BadRequest("Invalid email or verification code.");
                }

                return Ok(new { Message = "Clerk email verified successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying clerk email");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("casual/register")]
        public async Task<IActionResult> RegisterCasualUser([FromBody] RegisterCasualUserDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterCasualUserAsync(registerDto);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register casual user: {Errors}", result.Errors);
                    return BadRequest(result.Errors);
                }

                return Ok(new { Message = "Casual user registration successful. Please check your email for verification code." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering casual user");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("casual/verify")]
        public async Task<IActionResult> VerifyCasualUserEmail([FromBody] VerifyEmailDto verifyDto)
        {
            try
            {
                var isVerified = await _authService.VerifyCasualUserEmailAsync(verifyDto);

                if (!isVerified)
                {
                    _logger.LogWarning("Failed to verify casual user email: {Email}", verifyDto.Email);
                    return BadRequest("Invalid email or verification code.");
                }

                return Ok(new { Message = "Casual user email verified successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying casual user email");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("doctor/register")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterDoctorAsync(registerDto);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register doctor: {Errors}", result.Errors);
                    return BadRequest(result.Errors);
                }

                return Ok(new
                {
                    Message = "Doctor registration submitted successfully. Your account will be activated after approval by Medical Syndicate."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering doctor");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        // يمكنك إضافة endpoints أخرى مثل login هنا
    }
}