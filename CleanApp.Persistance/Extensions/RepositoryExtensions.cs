using CleanApp.Application.Contracts.Persistence;
using CleanApp.Domain.Options;
using CleanApp.Persistence.Categories;
using CleanApp.Persistence.Interceptors;
using CleanApp.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApp.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings = configuration.GetSection
                (ConnectionStringOption.Key).Get<ConnectionStringOption>();
                //tutorial uses sql server
                options.UseNpgsql(connectionStrings!.Postgres, npgsqlOptionsAction =>
                {
                    npgsqlOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                }
                );

                options.AddInterceptors(new AuditDbContextInterceptor());

            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
