using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers;

public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
{
    public Task Consume(ConsumeContext<ProductAddedEvent> context)
    {
        Console.WriteLine($"ProductAddedEventConsumer: {context.Message.Id}, {context.Message.Name}, {context.Message.Price}");
        return Task.CompletedTask;
    }
}