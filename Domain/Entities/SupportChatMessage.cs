﻿namespace Domain.Entities;

public class SupportChatMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string RoomId { get; set; }
}