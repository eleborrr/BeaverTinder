using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.Chat.GetChatById;

public record GetChatByIdQuery(string CurrUserId, string UserId) : IQuery<Room?>
{
}