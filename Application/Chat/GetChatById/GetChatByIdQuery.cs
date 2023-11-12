using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Chat.GetChatById;

public record GetChatByIdQuery(string CurrUserId, string UserId) : IQuery<Room?>
{
}