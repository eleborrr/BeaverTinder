using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using MediatR;

namespace BeaverTinder.Application.Features.Chat.SaveMessage;

public record SaveChatMessageByDtoBusCommand(ChatMessageDto Message) : ICommand<Unit>, ICommand<ChatMessageDto>;
