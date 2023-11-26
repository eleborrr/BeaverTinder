namespace BeaverTinder.Application.Dto.SupportChat;

public class SupportChatMessageDto
{
    public string Content { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string SenderId { get; set; } = default!;
    public string ReceiverId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public string ReceiverName { get; set; } = default!;
}