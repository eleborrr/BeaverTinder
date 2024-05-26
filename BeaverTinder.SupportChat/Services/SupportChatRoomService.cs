using System.Collections.Concurrent;
using BeaverTinder.Shared;
using BeaverTinder.Shared.Files;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;

namespace BeaverTinder.SupportChat.Services;

public class SupportChatRoomService : ISupportChatRoomService
{
    private static readonly ConcurrentDictionary<string, List<ChatClient>> _chatRooms = new ConcurrentDictionary<string, List<ChatClient>>();
    

    public async Task AddClientToChatRoom(string chatRoomId, ChatClient chatClient)
    {
        if (!_chatRooms.ContainsKey(chatRoomId))
        {
            _chatRooms[chatRoomId] = new List<ChatClient> { chatClient };
        }
        else
        {
            var existingUser = _chatRooms[chatRoomId].FirstOrDefault(c => c.UserName == chatClient.UserName);
            if (existingUser == null)
            {
                _chatRooms[chatRoomId].Add(chatClient);
            }
        }

        await Task.CompletedTask;
    }

    public async Task RemoveClientFromChatRoom(string chatRoomId, ChatClient chatClient)
    {
        _chatRooms[chatRoomId].Remove(chatClient);
        await Task.CompletedTask;
    }
    
    public async Task BroadcastMessageToChatRoom(MessageGrpc messageGrpc)
    {
        if (_chatRooms.ContainsKey(messageGrpc.GroupName))
        {
            var tasks = new List<Task>();
            foreach (var client in _chatRooms[messageGrpc.GroupName])
            {
                tasks.Add(client.StreamWriter.WriteAsync(messageGrpc));
            }

            await Task.WhenAll(tasks);
        }
    }
}


public class ChatClient
{
    public IServerStreamWriter<MessageGrpc> StreamWriter { get; set; }
    public string UserName { get; set; }
}