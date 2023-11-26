﻿namespace BeaverTinder.Domain.Entities;

public class SupportRoom
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string FirstUserId { get; set; } = default!;
    public string SecondUserId { get; set; } = default!;
    public ICollection<SupportChatMessage>? Messages { get; set; }
}