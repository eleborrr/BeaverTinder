namespace Contracts.ViewModels;

public class ChatModel
{
    public string RoomName { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string ReceiverId { get; set; } = default!;
}