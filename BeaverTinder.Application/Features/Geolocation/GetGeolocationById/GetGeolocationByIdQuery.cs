using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.Geolocation.GetGeolocationById;

public record GetGeolocationByIdQuery(string UserId): IQuery<GeolocationResponseDto>;