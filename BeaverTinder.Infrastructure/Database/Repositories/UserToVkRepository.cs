﻿using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public class UserToVkRepository : IUserToVkRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserToVkRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<UserToVk>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.UserToVks.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserToVk userToVk)
    {
        await _applicationDbContext.UserToVks.AddAsync(userToVk);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<UserToVk?> GetByIdAsync(string vkId)
    {
        return await _applicationDbContext.UserToVks.FirstOrDefaultAsync(x => x.VkId == vkId);
    }
}