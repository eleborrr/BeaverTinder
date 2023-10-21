using System.Text;
using System.Text.Json;
using Contracts.Configs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Services.RabbitMq;

public class RabbitMqSupChatConsumer : BackgroundService
{
    private readonly RabbitMqConfig _config;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqSupChatConsumer(
        IOptions<RabbitMqConfig> config,
        IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
        _config = config.Value;
        var factory = new ConnectionFactory { HostName = _config.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateChannel();
        _channel.QueueDeclare(queue: _config.SupChatQueueName, durable: true, exclusive: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            var supChatMess = JsonSerializer.Deserialize<SupportChatMessage>(content);

            await _repositoryManager.SupportChatMessageRepository.AddAsync(supChatMess!);
        };

        _channel.BasicConsume(_config.SupChatQueueName, false, consumer);

        return Task.CompletedTask;
    }
	
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}