﻿using Contracts.Configs;
using MassTransit;

namespace BeaverTinder.ServicesExtensions.MassTransit;

public static class ServiceCollectionExtension
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
        Console.WriteLine($"user: {rabbitConfiguration.Username}\n" +
                          $"Password: {rabbitConfiguration.Password}\n"+
                          $"Hostname: {rabbitConfiguration.Hostname}\n" +
                          $"Port: {rabbitConfiguration.Port}\n");
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var uri =
                    $"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}";
                configurator.Host(uri);
                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}