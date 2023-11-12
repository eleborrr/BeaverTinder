using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Commands;

namespace Application.SupportChat.CreateSupportChatById;

public class CreateSupportChatByIdHandler : ICommandHandler<CreateSupportChatByIdCommand, SupportRoom>
{
    private readonly IRepositoryManager _repositoryManager;

    public CreateSupportChatByIdHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<SupportRoom>> Handle(CreateSupportChatByIdCommand request, CancellationToken cancellationToken)
    {
        var supportRoom = new SupportRoom
        {
            Id = Guid.NewGuid().ToString(),
            FirstUserId = request.CurUserId,
            SecondUserId = request.UserId,
            Name = Guid.NewGuid().ToString()
        };
        try
        {
            await _repositoryManager.SupportRoomRepository.AddAsync(supportRoom);
            return new Result<SupportRoom>(supportRoom, true);
        }
        catch (Exception e)
        {
            return new Result<SupportRoom>(new SupportRoom(), false, e.Message);
        }
    }
}