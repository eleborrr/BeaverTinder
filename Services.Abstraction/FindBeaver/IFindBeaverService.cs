using Domain.Entities;

namespace Services.Abstraction.FindBeaver;

public interface IFindBeaverService
{
    public Task<User?> GetNextBeaver(User currentUser);
    public Task Like(string userId1, string userId2);
    public Task Dislike(string userId1, string userId2);
}