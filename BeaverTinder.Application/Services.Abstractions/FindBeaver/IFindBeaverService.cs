using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.FindBeaver;

public interface IFindBeaverService
{
    public Task<SearchUserResultDto> GetNextBeaver(User? currentUser, Role? userRole);
    public Task<LikeResponseDto> AddSympathy(User? user1, string userId2, bool sympathy,  Role? userRole);
    public Task<SearchUserResultDto> GetNextSympathy(User? currentUser);
}