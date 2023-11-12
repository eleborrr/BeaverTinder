using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetChatById;

public class GetSupportChatByIdQuery : IQuery<SupportRoom>
{
    public string CurUserId { get; set; }
    public string UserId { get; set; }
}