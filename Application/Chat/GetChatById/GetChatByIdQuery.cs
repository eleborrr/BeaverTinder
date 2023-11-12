using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Chat.GetChatById;

public class GetChatByIdQuery : IQuery<Room?>
{
    public string CurrUserId { get; set; } = default!;
    public string UserId { get; set; } = default!;
}