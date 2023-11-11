using System.Web.Http;
using Domain.Entities;
using Features.Geolocation.GetGeolocations;
using MediatR;

namespace Presentation.Controllers;

public class GeolocationTestController
{
    private readonly IMediator _mediator;

    public GeolocationTestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserGeolocation>> GetAllGeolocations(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllGeolocationsQuery(), cancellationToken);
        return response.Value;
    }
    
    // [HttpGet]
    // public Task GetGeolocationById()
    // {
    //     
    // }
    //
    // [HttpPost]
    // public Task AddGeolocation()
    // {
    //     
    // }
    //
    // [HttpPut]
    // public Task UpdateGeolocation()
    // {
    //     
    // }
}