using Contracts.Dto.SupportChat;
using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.SupportChat.SaveMessageByDtoBus;

public record SaveMessageByDtoBusCommand(SupportChatMessageDto Message) : ICommand<Unit>, ICommand<SupportChatMessageDto>;