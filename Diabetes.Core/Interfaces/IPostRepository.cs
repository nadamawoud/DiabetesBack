using Diabetes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsWithOrganizationAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<Organization> GetOrganizationByUserIdAsync(string userId);
    }

}
