using Scalar.AspNetCore;

namespace App.Api.Extensions;

public static class DocumentationExtensions
{
    public static IServiceCollection AddDocumentationExtension(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static IEndpointRouteBuilder UseScalarExtension(this IEndpointRouteBuilder app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
        return app;
    }
}