using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Repository
{
   public static class RoleSeeder
   {
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = {
            "Admin", "Manager", "Doctor", "Clerk", "CasualUser",
            "MedicalSyndicate", "NationalInstitute", "MinistryOfHealth",
            "WorldHealthOrganization", "GeneralAuthority"
        };

        foreach (var role in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
  }

}
