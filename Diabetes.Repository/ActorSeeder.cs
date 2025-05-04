using Diabetes.Core.Entities;
using Diabetes.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Repository
{
    public static class ActorSeeder
    {
        public static async Task SeedActorsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // ➤ Admins
            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            var admins = new List<Admin>();

            foreach (var user in adminUsers)
            {
                if (!context.Admins.Any(a => a.AppUserId == user.Id))
                {
                    admins.Add(new Admin { AppUserId = user.Id });
                }
            }

            if (admins.Any())
            {
                await context.Admins.AddRangeAsync(admins);
                await context.SaveChangesAsync();
            }

            // ➤ Managers
            var managerUsers = await userManager.GetUsersInRoleAsync("Manager");
            foreach (var user in managerUsers)
            {
                if (!context.Managers.Any(m => m.AppUserId == user.Id))
                {
                    // لا تشترط وجود أدمن مرتبط
                    context.Managers.Add(new Manager
                    {
                        AppUserId = user.Id
                    });
                }
            }

            await context.SaveChangesAsync();

            // ➤ Organizations
            var orgRoles = new Dictionary<string, bool>
    {
        {"MedicalSyndicate", true},
        {"NationalInstitute", false},
        {"MinistryOfHealth", false},
        {"WorldHealthOrganization", false},
        {"GeneralAuthority", false}
    };

            // في جزء Organizations:
foreach (var (role, isMedical) in orgRoles)
            {
                var users = await userManager.GetUsersInRoleAsync(role);
                var firstAdmin = await context.Admins.FirstOrDefaultAsync();

                if (firstAdmin == null)
                {
                    throw new Exception("لا يوجد أي أدمن في النظام!");
                }

                foreach (var user in users)
                {
                    if (!context.Organizations.Any(o => o.AppUserId == user.Id))
                    {
                        context.Organizations.Add(new Organization
                        {
                            AppUserId = user.Id,
                            IsMedicalSyndicate = isMedical,
                            AdminID = firstAdmin.ID // تعيين أول أدمن موجود
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }


}
