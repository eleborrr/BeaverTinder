using Contracts.Dto.MediatR;
using Contracts.Dto.Subscription;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Subscription.GetUsersActiveSubscription;

public class GetUsersActiveSubscriptionHandler: IQueryHandler<GetUsersActiveSubscriptionQuery, SubscriptionInfoDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;

    public GetUsersActiveSubscriptionHandler(UserManager<User> userManager, IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<SubscriptionInfoDto>> Handle(GetUsersActiveSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var roles = await _userManager.GetRolesAsync(user!);
        if (roles.Any(c => c == "Admin"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto()
            {
                Name = "Admin",
                Expires = new DateTime(10, 10, 10)
            }, true);
        }
        if (roles.Any(c => c == "Moderator"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto()
            {
                Name = "Moderator",
                Expires = new DateTime(10, 10, 10)
            }, true);
        }
        var userSub = (await _repositoryManager.UserSubscriptionRepository.GetActiveSubscriptionsByUserIdAsync(request.UserId))
            .FirstOrDefault();
        if (userSub == null)
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto()
            {
                Name = "User",
                Expires = new DateTime(10, 10, 10)
            }, true);
        }
        var sub = await _repositoryManager.SubscriptionRepository.GetBySubscriptionIdAsync(userSub.SubsId);
        return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto()
        {
            Name = sub!.Name,
            Expires = userSub.Expires
        }, true);
    }
}