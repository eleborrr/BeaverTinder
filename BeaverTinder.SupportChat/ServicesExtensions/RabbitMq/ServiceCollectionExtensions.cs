using BeaverTinder.Application.Configs;
using BeaverTinder.SupportChat.Services;
using MassTransit;
using RabbitMQ.Client;

namespace BeaverTinder.SupportChat.ServicesExtensions.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitConfiguration = new RabbitMqConfig
        {
            Hostname = configuration["MessageBroker:Hostname"]!,
            Password = configuration["MessageBroker:Password"]!,
            Username = configuration["MessageBroker:Username"]!,
            Port = configuration["MessageBroker:Port"]!
        };
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<SupportChatConsumer>();
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var uri =
                    $"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}";
                configurator.Host(uri);
                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddLikeMade(rabbitConfiguration);
        return services;
    }
    
    public static IServiceCollection AddLikeMade(this IServiceCollection services, RabbitMqConfig config)
    {
        services.AddSingleton(_ =>
        {
            var factory = new ConnectionFactory
            {
                HostName = config.Hostname,
                CredentialsProvider = new BasicCredentialsProvider(config.Username, config.Password)
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(Shared.StaticValues.Clickhouse.ExchangeName, ExchangeType.Direct);

            return channel;
        });

        return services;
    }
}