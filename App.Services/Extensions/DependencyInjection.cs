using Microsoft.Extensions.DependencyInjection;
using Services.Products;

namespace Services.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}