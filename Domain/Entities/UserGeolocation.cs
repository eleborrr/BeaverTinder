﻿namespace Domain.Entities;

public class UserGeolocation
{
    public int Id { get; set; } // same as userId
    public double Latutide { get; set; }
    public double Longtitude { get; set; }
}