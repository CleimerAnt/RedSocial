using Microsoft.AspNetCore.Identity;
using RedSocial.Core.Application.Enums;
using RedSocial.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.UserName = "SuperAdmin";

            defaultUser.Email = "SuperAdmin@gmail.com";

            defaultUser.FirstName = "Clei";

            defaultUser.LastName = "Lorenzo";

            defaultUser.PhoneNumber = "809-000-0000";

            defaultUser.ImgUrl = string.Empty;

            defaultUser.EmailConfirmed = true;

            defaultUser.PhoneNumberConfirmed = true;


            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var usuario = userManager.FindByEmailAsync(defaultUser.Email);
                if (usuario != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
            }

        }
    }
}
