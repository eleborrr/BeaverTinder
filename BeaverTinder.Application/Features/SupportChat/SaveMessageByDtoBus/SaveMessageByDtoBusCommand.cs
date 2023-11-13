using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using MediatR;

namespace Application.SupportChat.SaveMessageByDtoBus;

public record SaveMessageByDtoBusCommand(SupportChatMessageDto Message) : ICommand<Unit>, ICommand<SupportChatMessageDto>;