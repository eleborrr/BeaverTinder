using System.Collections;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Like.GetLikes;

public class GetAllLikesQuery : IQuery<IEnumerable<Domain.Entities.Like>>
{
    
}