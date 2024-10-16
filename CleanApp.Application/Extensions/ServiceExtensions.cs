using CleanApp.Application.Features.Categories;
using CleanApp.Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanApp.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            //TODO : Move Api layer
            //services.AddScoped(typeof(NotFoundFilter<,>));
            //services.AddExceptionHandler<CriticalExceptionHandler>();
            //services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }

    }
}
