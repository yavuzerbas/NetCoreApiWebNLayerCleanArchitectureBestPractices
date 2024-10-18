using CleanApp.API.FIlters;

namespace CleanApp.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllerWithFiltersExt(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            services.AddScoped(typeof(NotFoundFilter<,>));
            return services;
        }
    }
}
