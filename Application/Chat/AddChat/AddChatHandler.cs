using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Chat.AddChat;

public class AddChatHandler : ICommandHandler<AddChatCommand, Guid>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddChatHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<Guid>> Handle(AddChatCommand request, CancellationToken cancellationToken)
    {
        var chatId = Guid.NewGuid();
        var room = new Room
        {
            Id = chatId.ToString(),
            FirstUserId = request.FirstUserId,
            SecondUserId = request.SecondUserId,
            Name = Guid.NewGuid().ToString()
        };
        try
        {
            await _repositoryManager.RoomRepository.AddAsync(
                room);
            return new Result<Guid>(chatId, true);
        }
        catch (Exception e)
        {
            return new Result<Guid>(Guid.Empty, false, e.Message);
        }
    }
}