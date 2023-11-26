using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.OAuth.GetUserFromToVkById;

public record GetUserFromVkByIdQuery(string VkId) : IQuery<UserToVk?>;