using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application
{
    public static class ServiceRegistrator
    {
        public static void AddApplucationLayer(this IServiceCollection services, IConfiguration configuration)
        {
           
            #region "Services"
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IPublicationServices, PublicationServices>();

            services.AddTransient<IDboUserServices, DboUserServices>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICommentServices, CommentServices>();

            services.AddTransient<IReplyServices, ReplyServices>();

            services.AddTransient<IFriendsServices, FriendsServices>();
            #endregion
        }
    }
}
