﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class LikeRepository: ILikeRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public LikeRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
    
    public async Task<IEnumerable<Like>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _applicationDbContext.Likes.ToListAsync(cancellationToken);
    
    public async Task<Like?> GetByIdAsync(int ownerId, CancellationToken cancellationToken = default) =>
        await _applicationDbContext.Likes.FirstOrDefaultAsync(x => x.Id == ownerId, cancellationToken);

    public async Task AddAsync(Like like)
    {
        await _applicationDbContext.Likes.AddAsync(like);
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Remove(Like like) => _applicationDbContext.Likes.Remove(like);
    
}