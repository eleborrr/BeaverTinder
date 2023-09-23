using System.Security.Claims;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Subscriptions;

namespace Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;

    public SubscriptionService(IRepositoryManager repositoryManager, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }
    
    //TODO сделать проверку Expires(Expires not valid => active=false, delete role)
    //TODO самостоятельная деактивация подписки??
    public async Task AddSubscriptionToUser(int subsId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var sub = await _repositoryManager.SubscriptionRepository.GetBySubscriptionIdAsync(subsId);
        var userSub =
            await _repositoryManager.UserSubscriptionRepository.GetUserSubscriptionByUserIdAndSubsIdAsync(subsId, userId);
        if (userSub == null)
        {
            await _repositoryManager.UserSubscriptionRepository.AddUserSubscriptionAsync(subsId, userId);
            await _userManager.AddToRoleAsync(user!, sub!.RoleName);
            await _userManager.AddClaimAsync(user!, new Claim(ClaimTypes.Role, sub.RoleName));
            return;
        }
        if (userSub.Active)
        {
            var exp = userSub.Expires;
            userSub.Expires = exp + TimeSpan.FromDays(30);
            await _repositoryManager.UserSubscriptionRepository.SaveAsync();
            return;
        }
        await _repositoryManager.UserSubscriptionRepository.UpdateUserSubAsync(subsId, userId);
        await _userManager.AddToRoleAsync(user!, sub!.RoleName);
    }

    public async Task<SubInfoDto> GetUserActiveSubscription(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user!);
        if (roles.Any(c => c == "Admin"))
        {
            return new SubInfoDto()
            {
                Name = "Admin",
                Expires = new DateTime(10, 10, 10)
            };
        }
        if (roles.Any(c => c == "Moderator"))
        {
            return new SubInfoDto()
            {
                Name = "Moderator",
                Expires = new DateTime(10, 10, 10)
            };
        }
        var userSub = (await _repositoryManager.UserSubscriptionRepository.GetActiveSubscriptionsByUserIdAsync(userId))
            .FirstOrDefault();
        if (userSub == null)
        {
            return new SubInfoDto()
            {
                Name = "User",
                Expires = new DateTime(10, 10, 10)
            };
        }
        var sub = await _repositoryManager.SubscriptionRepository.GetBySubscriptionIdAsync(userSub.SubsId);
        return new SubInfoDto()
        {
            Name = sub!.Name,
            Expires = userSub.Expires
        };
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _repositoryManager.SubscriptionRepository.GetAllASync(default);
    }
}