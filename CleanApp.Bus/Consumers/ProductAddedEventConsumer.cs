using CleanApp.Domain.Events;
using MassTransit;

namespace CleanApp.Bus.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Incoming event:{context.Message.Id} - {context.Message.Name} - {context.Message.Price}");

            return Task.CompletedTask;
        }
    }
}
