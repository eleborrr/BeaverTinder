using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IUserToVkRepository
{
    public Task<IEnumerable<UserToVk>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(UserToVk userToVk);
    public Task<UserToVk?> GetByIdAsync(string vkId);
}