using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Account;
using RedSocial.Infraestructure.Identity.Context;
using RedSocial.Infraestructure.Identity.Entities;
using RedSocial.Infraestructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity
{
    public static class ServiceRegistrator 
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Identityconexion"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });


            #region "Identity"
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

          

          

            #endregion

            services.AddAuthentication();

            #region "Services"
            services.AddTransient<IAccountServices, AccountService>();
            #endregion

        }
    }
}
