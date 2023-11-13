using Domain.Entities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.SupportChat.CreateSupportChatById;

public class CreateSupportChatByIdCommand : ICommand<SupportRoom>
{
    public string CurUserId { get; set; }
    public string UserId { get; set; }
}