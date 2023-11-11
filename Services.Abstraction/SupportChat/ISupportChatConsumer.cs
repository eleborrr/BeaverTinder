using Domain.Entities;
using MassTransit;

namespace Services.Abstraction.SupportChat;

public interface ISupportChatConsumer: IConsumer<SupportChatMessage>
{
}