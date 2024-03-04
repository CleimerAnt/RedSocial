using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Infraestructure.Persitence.Context;
using RedSocial.Infraestructure.Persitence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region "Context"
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conexion"), m =>
                m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });
            #endregion

            #region "Repositories"
            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddTransient<IPublicationsRepository, PublicationsRepository>();
            services.AddTransient<IDboUserRepository, DboUserRepository>();
            services.AddTransient<ICommentsRepository, CommentRepository>();  
            services.AddTransient<IReplyRepository, RepliRepository>();

            services.AddTransient<IRepositoryFriends, FriendsRepository>();
            #endregion
        }
    }
}
