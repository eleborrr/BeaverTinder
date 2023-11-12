using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetChatById;

public class GetSupportChatByIdHandler : IQueryHandler<GetSupportChatByIdQuery, SupportRoom>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetSupportChatByIdHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<SupportRoom>> Handle(GetSupportChatByIdQuery request, CancellationToken cancellationToken)
    {
        var allRooms = _repositoryManager.SupportRoomRepository.GetAll();
        var room = await allRooms
            .FirstOrDefaultAsync(r => (r.FirstUserId == request.CurUserId && r.SecondUserId == request.UserId)
                                 ||
                                 (r.SecondUserId == request.CurUserId && r.FirstUserId == request.UserId));
        if (room is null)
        {
            return new Result<SupportRoom>(null, false, "can not find room");
        }

        return new Result<SupportRoom>(room, true, null);
    }
}