﻿namespace BeaverTinder.Domain.Entities;

public class UserSubscription
{
    public string UserId { get; set; } = null!;
    public int SubsId { get; set; }
    public DateTime Expires { get; set; }
    public bool Active { get; set; }
}