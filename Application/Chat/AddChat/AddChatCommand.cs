using Domain.Entities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Chat.AddChat;

public record AddChatCommand(string FirstUserId, string SecondUserId) : ICommand<Room>
{
}