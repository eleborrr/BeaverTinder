using Domain.Entities;
using Domain.Repositories;
using MassTransit;

namespace Services.SupportChat;

public class SupportChatConsumer : IConsumer<SupportChatMessage>
{
    private readonly ISupportChatMessageRepository _messageRepository;

    public SupportChatConsumer(ISupportChatMessageRepository supportChatMessageRepository)
    {
        _messageRepository = supportChatMessageRepository;
    }
    public async Task Consume(ConsumeContext<SupportChatMessage> context)
    {
        var message = context.Message;
        await _messageRepository.AddAsync(message);
    }
}