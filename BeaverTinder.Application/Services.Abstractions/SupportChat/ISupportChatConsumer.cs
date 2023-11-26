using BeaverTinder.Domain.Entities;
using MassTransit;

namespace BeaverTinder.Application.Services.Abstractions.SupportChat;

public interface ISupportChatConsumer: IConsumer<SupportChatMessage>
{
}