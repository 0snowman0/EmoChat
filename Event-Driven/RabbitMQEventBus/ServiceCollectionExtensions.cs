using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQEventBus.Contracts;
using RabbitMQEventBus.Implementations;
using Microsoft.Extensions.Configuration;

namespace RabbitMQEventBus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQEventBus(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = new RabbitMQSettings();
            configuration.GetSection("RabbitMQ").Bind(settings);

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(settings.Host, h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });

                    cfg.UseMessageRetry(r => r.Interval(settings.RetryCount, 200));
                    cfg.PrefetchCount = settings.PrefetchCount;
                });
            });

            #region Add Dep
            services.AddScoped<IEventBus, RabbitMQEventBus.Implementations.RabbitMQEventBus>();
            #endregion

            return services;
        }
    }
}