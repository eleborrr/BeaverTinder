namespace Contracts;

public class SupportChatMessageDto
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string RoomId { get; set; }
}