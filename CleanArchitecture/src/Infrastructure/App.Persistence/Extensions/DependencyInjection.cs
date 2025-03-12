using App.Application.Contracts.Persistence;
using App.Domain.Options;
using App.Persistence.Categories;
using App.Persistence.Interceptors;
using App.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            builder.UseSqlServer(connectionStrings!.SqlServer,
                optionsBuilder => { optionsBuilder.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName); });
            builder.AddInterceptors(new AuditDbContextInterceptor());
        });

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}