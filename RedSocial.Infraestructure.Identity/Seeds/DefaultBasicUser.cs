using Microsoft.AspNetCore.Identity;
using RedSocial.Core.Application.Enums;
using RedSocial.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {

            ApplicationUser defaultUser = new();

            defaultUser.UserName = "basicUser";

            defaultUser.Email = "basicUser@gmail.com";

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
                }
            }

        }
    }
}
      