using Microsoft.Extensions.DependencyInjection;
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
