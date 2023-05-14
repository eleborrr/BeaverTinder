using Contracts.Responses.Search;
using Domain.Entities;

namespace Services.Abstraction.FindBeaver;

public interface IFindBeaverService
{
    public Task<SearchUserResultDto> GetNextBeaver(User? currentUser, Role? userRole);
    public Task<LikeResponseDto> AddSympathy(User? user1, string userId2, bool sympathy,  Role? userRole);
    // public Task Dislike(string userId1, string userId2);
}