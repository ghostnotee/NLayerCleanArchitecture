using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            builder.UseSqlServer(connectionStrings!.SqlServer,
                optionsBuilder => { optionsBuilder.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName); });
        });
        return services;
    }
}