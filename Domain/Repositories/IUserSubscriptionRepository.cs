﻿using Domain.Entities;

namespace Domain.Repositories;

public interface IUserSubscriptionRepository
{
    public Task<IEnumerable<UserSubscription>> GetAllAsync(CancellationToken cancellationToken);
    public Task<List<UserSubscription>> GetSubscriptionsByUserIdAsync(string UserId);
    public Task<List<UserSubscription>> GetActiveSubscriptionsByUserIdAsync(string UserId);
    public Task<UserSubscription?> GetUserSubscriptionByUserIdAndSubsIdAsync(int subsId, string userId);
    public Task AddUserSubscriptionAsync(int subsId, string userId);
    public Task UpdateUserSubAsync(int subsId, string userId);
    public Task SaveAsync();
}