using App.Api.Filters;

namespace App.Api.Extensions;

public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithFiltersExtension(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });
        return services;
    }
}