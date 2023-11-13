using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Chat.AddChat;

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