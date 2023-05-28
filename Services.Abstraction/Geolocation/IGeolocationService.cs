﻿using Domain.Entities;

namespace Services.Abstraction.Geolocation;

public interface IGeolocationService
{
    public Task AddAsync(string userId, double Latitude, double Longitude);

    public Task<UserGeolocation> GetByUserId(string userId);

    public Task<IEnumerable<UserGeolocation>> GetAllAsync();
    public Task<double> GetDistance(UserGeolocation geolocation1, UserGeolocation geolocation2);
    public Task<double> GetDistance(User user1, User user2);
}