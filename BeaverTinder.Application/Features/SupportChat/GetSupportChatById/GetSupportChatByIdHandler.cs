using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Application.Features.SupportChat.GetSupportChatById;

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
            .FirstOrDefaultAsync(r => 
                (r.FirstUserId == request.CurUserId && r.SecondUserId == request.UserId) ||
                (r.SecondUserId == request.CurUserId && r.FirstUserId == request.UserId),
                cancellationToken);
        if (room is null)
        {
            return new Result<SupportRoom>(null, false, "can not find room");
        }

        return new Result<SupportRoom>(room, true, null);
    }
}