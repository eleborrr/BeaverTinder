using System.Security.Claims;
using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Application.Services.Abstractions.Subscriptions;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly DateTime _defaultDateTime =
        new(10, 10, 10, 0, 0, 0, DateTimeKind.Utc);

    public SubscriptionService(IRepositoryManager repositoryManager, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }
    
    public async Task AddSubscriptionToUser(int subsId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var sub = await _repositoryManager
            .SubscriptionRepository.GetBySubscriptionIdAsync(subsId);
        var userSub =
            await _repositoryManager.UserSubscriptionRepository
                .GetUserSubscriptionByUserIdAndSubsIdAsync(subsId, userId);
        if (userSub == null)
        {
            await _repositoryManager.UserSubscriptionRepository
                .AddUserSubscriptionAsync(subsId, userId);
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

    public async Task<SubscriptionInfoDto> GetUserActiveSubscription(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user!);
        if (roles.Any(c => c == "Admin"))
        {
            return new SubscriptionInfoDto
            {
                Name = "Admin",
                Expires = _defaultDateTime
            };
        }
        if (roles.Any(c => c == "Moderator"))
        {
            return new SubscriptionInfoDto
            {
                Name = "Moderator",
                Expires = _defaultDateTime
            };
        }
        var userSub = (await _repositoryManager.UserSubscriptionRepository.GetActiveSubscriptionsByUserIdAsync(userId))
            .FirstOrDefault();
        if (userSub == null)
        {
            return new SubscriptionInfoDto
            {
                Name = "User",
                Expires = _defaultDateTime
            };
        }
        var sub = await _repositoryManager.SubscriptionRepository.GetBySubscriptionIdAsync(userSub.SubsId);
        return new SubscriptionInfoDto
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