using Diabetes.Core.Entities;
using Diabetes.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Repository.Repositories
{
    public class PostRepository
    {
        private readonly StoreContext _context;
        public PostRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsWithOrganizationAsync()
        {
            return await _context.Posts
                .Include(p => p.Organization)
                    .ThenInclude(o => o.AppUser)
                .ToListAsync();
        }
    }

}
