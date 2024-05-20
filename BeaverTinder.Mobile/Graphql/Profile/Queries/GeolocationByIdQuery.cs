using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Mobile.Helpers.PolicyStrings;
using BeaverTinder.Application.Dto.Geolocation;
using HotChocolate.Authorization;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    [Authorize(Policy = PolicyStaticStrings.MapSubs)]
    public async Task<GeolocationResponseDto?> GetUserGeolocation(GetGeolocationRequestDto model)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return (await mediator.Send(new GetGeolocationByIdQuery(model.UserId))).Value;
    }
}