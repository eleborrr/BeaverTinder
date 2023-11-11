using Contracts.Dto.Geolocation;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Geolocation.GetGeolocationById;

public class GetGeolocationByIdQuery: IQuery<GeolocationResponseDto>
{
    public string UserId { get; set; } = default!;

}