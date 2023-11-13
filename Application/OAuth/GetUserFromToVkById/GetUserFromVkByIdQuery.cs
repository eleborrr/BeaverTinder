using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAuth.GetUserFromToVkById;

public record GetUserFromVkByIdQuery(string VkId) : IQuery<UserToVk?>;