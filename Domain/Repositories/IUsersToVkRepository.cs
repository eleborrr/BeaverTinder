using Domain.Entities;

namespace Domain.Repositories;

public interface IUserToVkRepository
{
    public Task<IEnumerable<UserToVk>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(UserToVk userToVk);
    public Task<UserToVk?> GetByIdAsync(string vkId);
}