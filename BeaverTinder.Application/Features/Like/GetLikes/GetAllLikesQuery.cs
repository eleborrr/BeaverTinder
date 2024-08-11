using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.Like.GetLikes;

public class GetAllLikesQuery : IQuery<IEnumerable<Domain.Entities.Like>>
{
    
}