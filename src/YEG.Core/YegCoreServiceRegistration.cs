using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.Core.Security.JWT;

namespace YEG.Core
{
    public static class YegCoreServiceRegistration
    {
        public static IServiceCollection AddYegCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();

            return services;
        }
    }
}
