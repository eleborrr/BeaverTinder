using Application.OAuth.AddUserToVk;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using MediatR;

namespace BeaverTinder.Application.Features.OAuth.AddUserToVk;

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