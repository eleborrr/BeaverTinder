namespace Contracts.Configs;

public class RabbitMqConfig
{
    public string UserName { get; set; } = default!;
    public string HostName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string VirtualHost { get; set; } = default!;
    public string SupChatQueueName { get; set; } = default!;
}