using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus;

public static class BusExtensions
{
    public static void AddBusExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(new Uri(serviceBusOption!.Url), hostConfigurator => { });
            });
        });
    }
}