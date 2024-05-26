using BeaverTinder.Shared;

namespace BeaverTinder.SupportChat.Services;

public interface ISupportChatRoomService
{
    public Task AddClientToChatRoom(string chatRoomId, ChatClient chatClient);
    public Task RemoveClientFromChatRoom(string chatRoomId, ChatClient chatClient);
    public Task BroadcastMessageToChatRoom(MessageGrpc message);
}