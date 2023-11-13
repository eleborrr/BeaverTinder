using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.Chat.AddChat;

public record AddChatCommand(string FirstUserId, string SecondUserId) : ICommand<Room>
{
}