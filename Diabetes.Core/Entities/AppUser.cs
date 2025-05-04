using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class AppUser : IdentityUser
    {
        // Common properties
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; } // Male, Female, Other
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation to actors
        public Manager? Manager { get; set; }
        public Admin? Admin { get; set; }
        public Clerk? Clerk { get; set; }
        public Doctor? Doctor { get; set; }
        public CasualUser? CasualUser { get; set; }
        public Organization? Organization { get; set; }
    }
}
