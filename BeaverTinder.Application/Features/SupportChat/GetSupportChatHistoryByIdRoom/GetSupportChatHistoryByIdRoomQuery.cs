using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace Application.SupportChat.GetSupportChatHistory;

public record GetSupportChatHistoryByIdRoomQuery(string SupportRoomId) : IQuery<IEnumerable<ChatMessageDto>>;