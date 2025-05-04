using Diabetes.Core.DTOs;
using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Services.Services
{
    public class CreatePostService : ICreatePostService
    {
        private readonly StoreContext _context;

        public CreatePostService(StoreContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            var organization = _context.Organizations.FirstOrDefault(o => o.AppUserId == userId);
            if (organization == null)
                throw new Exception("Organization not found");

            var post = new Post
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                PublishDate = DateTime.UtcNow,
                OrganizationID = organization.ID
            };

            if (createPostDto.Image != null)
            {
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(createPostDto.Image.FileName)}";
                var imagePath = Path.Combine("wwwroot/uploads/images", imageName);

                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await createPostDto.Image.CopyToAsync(stream);
                }

                post.ImageURL = $"/uploads/images/{imageName}";
            }

            if (createPostDto.Video != null)
            {
                var videoName = $"{Guid.NewGuid()}{Path.GetExtension(createPostDto.Video.FileName)}";
                var videoPath = Path.Combine("wwwroot/uploads/videos", videoName);

                Directory.CreateDirectory(Path.GetDirectoryName(videoPath)!);

                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await createPostDto.Video.CopyToAsync(stream);
                }

                post.VideoURL = $"/uploads/videos/{videoName}";
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
