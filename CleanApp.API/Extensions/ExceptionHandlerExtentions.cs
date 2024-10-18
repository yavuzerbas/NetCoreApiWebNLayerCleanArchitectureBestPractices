using CleanApp.API.ExceptionHandlers;

namespace CleanApp.API.Extensions
{
    public static class ExceptionHandlerExtentions
    {
        public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
        {

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
