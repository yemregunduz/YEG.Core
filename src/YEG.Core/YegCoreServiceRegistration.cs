using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YEG.Core.Pipelines.Authorization;
using YEG.Core.Pipelines.Caching;
using YEG.Core.Pipelines.Logging;
using YEG.Core.Pipelines.Performance;
using YEG.Core.Security.JWT;

namespace YEG.Core
{
    public static class YegCoreServiceRegistration
    {
        public static IServiceCollection AddYegCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            return services;
        }
    }
}
