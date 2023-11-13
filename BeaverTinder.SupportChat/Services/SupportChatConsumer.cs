using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using MassTransit;

namespace BeaverTinder.SupportChat.Services;

public class SupportChatConsumer : IConsumer<SupportChatMessage>
{
    private readonly ISupportChatMessageRepository _messageRepository;

    public SupportChatConsumer(ISupportChatMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    public async Task Consume(ConsumeContext<SupportChatMessage> context)
    {
        var message = context.Message;
        await _messageRepository.AddAsync(message);
    }
}