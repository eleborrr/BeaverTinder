using Contracts;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using Persistence.Repositories;

namespace Services.SupportChat;

public class SupportChatConsumer : IConsumer<SupportChatMessage>
{
    private readonly IRepositoryManager _repositoryManager;

    public SupportChatConsumer(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task Consume(ConsumeContext<SupportChatMessage> context)
    {
        var message = context.Message;
        await _repositoryManager.SupportChatMessageRepository.AddAsync(message);
    }
}