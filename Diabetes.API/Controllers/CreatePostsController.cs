using Diabetes.Core.DTOs;
using Diabetes.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diabetes.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "MedicalSyndicate,NationalInstitute,MinistryOfHealth,WorldHealthOrganization,GeneralAuthority")]
    public class CreatePostsController : ControllerBase
    {
        private readonly ICreatePostService _createPostService;

        public CreatePostsController(ICreatePostService createPostService)
        {
            _createPostService = createPostService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            // Get the userId from the token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if userId is valid
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authorized" });

            try
            {
                // Call the service to create the post
                await _createPostService.CreatePostAsync(createPostDto, userId);
                return Ok(new { message = "Post created successfully." });
            }
            catch (Exception ex)
            {
                // If there's an error, send a more descriptive response
                return BadRequest(new { message = "An error occurred while creating the post.", details = ex.Message });
            }
        }
    }
}

