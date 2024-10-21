using CleanApp.Application.Contracts.ServiceBus;
using CleanApp.Domain.Events;
using MassTransit;

namespace CleanApp.Bus
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        public async Task PublishAsync<T>(T @event, CancellationToken cancellation = default) where T : IEventOrMessage
        {
            await publishEndpoint.Publish(@event, cancellation);
        }

        public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellation = default) where T : IEventOrMessage
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(message, cancellation);
        }
    }
}
