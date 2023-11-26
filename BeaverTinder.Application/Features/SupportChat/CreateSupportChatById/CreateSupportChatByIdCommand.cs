using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.SupportChat.CreateSupportChatById;

public record CreateSupportChatByIdCommand(string CurUserId, string UserId) : ICommand<SupportRoom>;