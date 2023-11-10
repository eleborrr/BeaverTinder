namespace Domain.Entities;

public class SupportChatMessage
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string SenderId { get; set; } = default!;
    public string ReceiverId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
}