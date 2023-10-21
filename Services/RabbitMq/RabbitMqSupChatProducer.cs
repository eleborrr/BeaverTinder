using System.Text;
using System.Text.Json;
using Contracts.Configs;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Services.Abstraction.RabbitMq;
using RabbitMQ.Client;

namespace Services.RabbitMq;

public class RabbitMqSupChatProducer : IRabbitMqSupChatProducer
{
    private readonly RabbitMqConfig _config;

    public RabbitMqSupChatProducer(IOptions<RabbitMqConfig> config)
    {
        _config = config.Value;
    }

    public void SendingMessage(SupportChatMessage message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _config.HostName,
            UserName = _config.UserName,
            Password = _config.Password,
            VirtualHost = _config.VirtualHost
        };

        var conn = factory.CreateConnection();

        using var channel = conn.CreateChannel();

        //durable save queue if rabbitmq exit
        //exclusive means just one consumer is available 
        channel.QueueDeclare(
            _config.SupChatQueueName,
            durable: true,
            exclusive: false);

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);
        
        channel.BasicPublish("", _config.SupChatQueueName, body);
    }
}