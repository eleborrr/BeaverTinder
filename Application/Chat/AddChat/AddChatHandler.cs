using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Chat.AddChat;

public class AddChatHandler : ICommandHandler<AddChatCommand, Room>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddChatHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<Room>> Handle(AddChatCommand request, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            Id = Guid.NewGuid().ToString(),
            FirstUserId = request.FirstUserId,
            SecondUserId = request.SecondUserId,
            Name = Guid.NewGuid().ToString()
        };
        try
        {
            await _repositoryManager.RoomRepository.AddAsync(
                room);
            return new Result<Room>(room, true);
        }
        catch (Exception e)
        {
            return new Result<Room>(new Room(), false, e.Message);
        }
    }
}