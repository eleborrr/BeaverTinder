using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAth.GetUserFromToVkById;

public class GetUserFromVkByIdHandler : IQueryHandler<GetUserFromVkByIdQuery, UserToVk?>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetUserFromVkByIdHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<UserToVk?>> Handle(
        GetUserFromVkByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var vkUser = await _repositoryManager.UserToVkRepository.GetByIdAsync(request.VkId);
            if (vkUser is null)
                return new Result<UserToVk?>(null, false, "User not found");
            return new Result<UserToVk?>(vkUser, true);
        }
        catch (Exception e)
        {
            return new Result<UserToVk?>(null, false, e.Message);
        }
    }
            
}