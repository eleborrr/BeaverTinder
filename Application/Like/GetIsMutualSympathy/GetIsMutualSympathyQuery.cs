using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Like.GetIsMutualSympathy;

public class GetIsMutualSympathyQuery : IQuery<bool>
{
    public User user1 { get; set; }
    public User user2 { get; set; }

}