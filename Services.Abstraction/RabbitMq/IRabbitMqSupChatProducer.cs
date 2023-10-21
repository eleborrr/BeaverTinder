using Domain.Entities;

namespace Services.Abstraction.RabbitMq;

public interface IRabbitMqSupChatProducer
{
    void SendingMessage(SupportChatMessage message);
}