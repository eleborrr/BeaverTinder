using Domain.Entities;
using MassTransit;

namespace Services.Abstraction.RabbitMq;

public interface ISupportChatConsumer: IConsumer<SupportChatMessage>
{
}