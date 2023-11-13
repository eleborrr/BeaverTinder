using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Chat.GetChatById;

public class GetChatByIdHandler : IQueryHandler<GetChatByIdQuery, Room?>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetChatByIdHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<Room?>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var room = (await _repositoryManager.RoomRepository.GetAllAsync(default))
            .FirstOrDefault(r => (r.FirstUserId == request.CurrUserId && r.SecondUserId == request.UserId)
                                 ||
                                 (r.SecondUserId == request.CurrUserId && r.FirstUserId == request.UserId));
        
        if (room is null)
            return new Result<Room?>(null, false, "Room not found");
        
        return new Result<Room?>(room, true);
    }
}