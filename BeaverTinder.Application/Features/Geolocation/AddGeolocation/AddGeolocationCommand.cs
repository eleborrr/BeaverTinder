using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace Application.Geolocation.AddGeolocation;

public class AddGeolocationCommand: ICommand<GeolocationIdDto>
{
    public string UserId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}