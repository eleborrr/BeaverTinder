using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.LikesMade;

public record AddLikeMadeQuery : IQuery<bool>;