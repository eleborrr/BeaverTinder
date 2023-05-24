using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
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
            await _userManager.AddToRoleAsync(user, sub.RoleName);
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
        await _userManager.AddToRoleAsync(user, sub.RoleName);
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _repositoryManager.SubscriptionRepository.GetAllASync(default);
    }
}