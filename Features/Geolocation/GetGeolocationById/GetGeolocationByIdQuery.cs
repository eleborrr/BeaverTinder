using Contracts.Dto.Geolocation;
using Services.Abstraction.Cqrs.Queries;

namespace Features.Geolocation.GetGeolocationById;

public class GetGeolocationByIdQuery: IQuery<GeolocationResponseDto>
{
    public string UserId { get; set; } = default!;

}