using Domain.Entities;

namespace Services.Abstraction.FindBeaver;

public interface IFindBeaverService
{
    public Task<User?> GetNextBeaver(User currentUser, Role userRole);
    public Task AddSympathy(User user1, string userId2, bool sympathy,  Role userRole);
    // public Task Dislike(string userId1, string userId2);
}