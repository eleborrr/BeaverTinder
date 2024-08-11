using Application.OAuth.GetUserFromToVkById;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.OAuth.GetUserFromToVkById;

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