using CleanApp.Application.Contracts.Caching;
using CleanApp.Caching;

namespace CleanApp.API.Extensions
{
    public static class CachingExtensions
    {
        public static IServiceCollection AddCachingExt(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
