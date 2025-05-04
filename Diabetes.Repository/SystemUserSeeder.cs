using Diabetes.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Repository
{
    public static class SystemUserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var users = new List<(string Email, string FullName, string Role)>
        {
            ("nada.admin@example.com", "Nada Ahmed", "Admin"),
            ("nour.admin@example.com", "Nour Ali", "Admin"),
            ("norhan.admin@example.com", "Norhan", "Admin"),
            ("noura.admin@example.com", "Noura gamal", "Admin"),
            ("mohamed.manager@example.com", "Mohamed Hassan", "Manager"),
            ("salma.manager@example.com", "Salma Tarek", "Manager"),
            ("syndicate@example.com", "Medical Syndicate", "MedicalSyndicate"),
            ("institute@example.com", "National Institute", "NationalInstitute"),
            ("moh@example.com", "Ministry of Health", "MinistryOfHealth"),
            ("who@example.com", "World Health Organization", "WorldHealthOrganization"),
            ("insurance@example.com", "General Authority", "GeneralAuthority")
        };

            foreach (var (email, fullName, role) in users)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newUser = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newUser, "Default@123");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newUser, role);
                }
            }
        }
    }

}