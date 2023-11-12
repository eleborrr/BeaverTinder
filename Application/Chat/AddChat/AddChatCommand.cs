using Services.Abstraction.Cqrs.Commands;

namespace Application.Chat.AddChat;

public class AddChatCommand : ICommand<Guid>
{
    public string FirstUserId { get; set; } = default!;
    public string SecondUserId { get; set; } = default!;
}