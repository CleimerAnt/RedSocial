using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity.Seeds
{
    public static  class SeedsConfiguration
    {
        public async static Task AddIdentitySeedsInfraestrucuture(this IServiceProvider services)
        {
            #region "Seeds"
            using (var scope = services.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;
                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();


                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultBasicUser.SeedAsync(userManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);



                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }
    }
}
