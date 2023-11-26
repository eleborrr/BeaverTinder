using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.Like.GetIsMutualSympathy;

public record GetIsMutualSympathyQuery(User user1, User user2) : IQuery<bool>;