using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAth.AddUserToVk;

public class AddUserToVkHandler : ICommandHandler<AddUserToVkCommand, Unit>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddUserToVkHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<Unit>> Handle(AddUserToVkCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userToVk = new UserToVk
            {
                UserId = request.PlatformUserId,
                VkId = request.VkUserId
            };
            await _repositoryManager.UserToVkRepository.AddAsync(userToVk);
            return new Result<Unit>(new Unit(), true);
        }
        catch (Exception e)
        {
            return new Result<Unit>(new Unit(), false, e.Message);
        }
    }
}