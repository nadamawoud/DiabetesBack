using AutoMapper;
using Diabetes.Core.DTOs;
using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;
using Diabetes.Repository.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Services.Services
{
    public class PostService : IPostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsWithOrganizationAsync();

            return posts.Select(post => new PostDto
            {
                Id = post.ID,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                ImageURL = post.ImageURL,
                VideoURL = post.VideoURL,
                OrganizationName = post.Organization.AppUser.UserName // أو Name لو عندك
            });
        }
    }

}
