using Contracts.Dto.BeaverMatchSearch;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Cqrs.Queries;

namespace Application.FindBeaver.GetNextSympathy;

public class GetNextSympathyHandler : IQueryHandler<GetNextSympathyQuery, SearchUserResultDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    

    public GetNextSympathyHandler(IRepositoryManager repositoryManager, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result<SearchUserResultDto>> Handle(
        GetNextSympathyQuery request,
        CancellationToken cancellationToken)
    {
        var likes = (await _repositoryManager.LikeRepository.GetAllAsync(default)).ToList();
        

        var filteredBeavers = _userManager.Users.AsEnumerable()
            .Where(u => likes.Exists(l => l.UserId ==  u.Id && l.LikedUserId == request.CurrentUser!.Id )
                        && !likes.Exists(l => l.UserId == request.CurrentUser!.Id && l.LikedUserId ==  u.Id)
                        && u.Id != request.CurrentUser!.Id) // проверяем чтобы попадались лайкнутые
            .OrderBy(u => Math.Abs(request.CurrentUser!.DateOfBirth.Year - u.DateOfBirth.Year))
            .Take(10)
            .ToList();
        
        var returnUserCache = filteredBeavers.FirstOrDefault();
        if (returnUserCache is null)
            return new Result<SearchUserResultDto>(new SearchUserResultDto
            {
                Successful = false,
                Message = "Beaver queue error",
                StatusCode = 500
            }, false);
        return new Result<SearchUserResultDto>(new SearchUserResultDto
        {
            Id = returnUserCache.Id,
            About = returnUserCache.About ?? "",
            FirstName = returnUserCache.FirstName,
            LastName = returnUserCache.LastName,
            Gender = returnUserCache.Gender,
            Age = DateTime.Now.Year - returnUserCache.DateOfBirth.Year,
            Message = "ok",
            StatusCode = 200,
            Successful = true,
            Image = returnUserCache.Image!
        }, true); 
    }
}