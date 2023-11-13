using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAth.GetUserFromToVkById;

public record GetUserFromVkByIdQuery(string VkId) : IQuery<UserToVk?>;