using BeaverTinder.Application.Features.Geolocation.GetGeolocations;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GeolocationTestController: Controller
{
    private readonly IMediator _mediator;

    public GeolocationTestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserGeolocation>> GetAllGeolocations(
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetAllGeolocationsQuery(),
            cancellationToken);
        return response.Value!;
    }
}