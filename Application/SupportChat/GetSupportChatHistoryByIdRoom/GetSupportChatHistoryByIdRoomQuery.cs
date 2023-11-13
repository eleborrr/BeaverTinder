using Contracts.Dto.SupportChat;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetSupportChatHistory;

public record GetSupportChatHistoryByIdRoomQuery(string SupportRoomId) : IQuery<IEnumerable<SupportChatMessageDto>>;