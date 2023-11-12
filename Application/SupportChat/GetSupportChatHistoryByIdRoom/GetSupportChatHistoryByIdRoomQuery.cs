using Contracts.Dto.SupportChat;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetSupportChatHistory;

public class GetSupportChatHistoryByIdRoomQuery : IQuery<IEnumerable<SupportChatMessageDto>>
{
    public string SupportRoomId { get; set; }
}