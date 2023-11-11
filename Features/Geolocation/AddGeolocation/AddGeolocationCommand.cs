using Contracts.Dto.Geolocation;
using Services.Abstraction.Cqrs.Commands;

namespace Features.Geolocation.AddGeolocation;

public class AddGeolocationCommand: ICommand<GeolocationIdDto>
{
    public string UserId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}