using Contracts.Dto.SupportChat;
using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.SupportChat.SaveMessageByDtoBus;

public class SaveMessageByDtoBusCommand : ICommand<Unit>, ICommand<SupportChatMessageDto>
{
    public SupportChatMessageDto Message { get; set; }
}