using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using MediatR;

namespace BeaverTinder.Application.Features.SupportChat.SaveMessageByDtoBus;

public record SaveMessageByDtoBusCommand(ChatMessageDto Message) : ICommand<Unit>, ICommand<ChatMessageDto>;