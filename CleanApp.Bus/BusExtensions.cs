using CleanApp.Application.Contracts.ServiceBus;
using CleanApp.Bus.Consumers;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApp.Bus
{
    public static class BusExtensions
    {
        public static void AddBusExt(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url), h => { });

                    cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<ProductAddedEventConsumer>(context);
                    });
                });
            });
        }
    }
}
