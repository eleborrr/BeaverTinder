using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.SupportChat.GetAllSupportChatRooms;

public class GetAllSupportChatRoomsHandler : IQueryHandler<GetAllSupportChatRoomsQuery, IEnumerable<SupportRoom>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllSupportChatRoomsHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<IEnumerable<SupportRoom>>> Handle(GetAllSupportChatRoomsQuery request, CancellationToken cancellationToken)
    {
        var supportRooms = (IEnumerable<SupportRoom>) _repositoryManager.SupportRoomRepository.GetAll();
        return new Result<IEnumerable<SupportRoom>>(supportRooms, true);
    }
}