using CleanApp.Domain.Events;

namespace CleanApp.Application.Contracts.ServiceBus
{
    public interface IServiceBus
    {
        /**
         *Sends to the exchange
         */
        Task PublishAsync<T>(T @event, CancellationToken cancellation = default) where T : IMessage, IEvent;

        /**
         *Sends directly to the queue 
        */
        Task SendAsync<T>(T message, string queueName, CancellationToken cancellation = default) where T : IMessage, IEvent;
    }
}
