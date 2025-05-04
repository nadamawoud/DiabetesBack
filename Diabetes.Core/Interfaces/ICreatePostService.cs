using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Diabetes.Core.DTOs;


namespace Diabetes.Core.Interfaces
{

    public interface ICreatePostService
    {
        Task CreatePostAsync(CreatePostDto createPostDto, string userId);
    }

}
