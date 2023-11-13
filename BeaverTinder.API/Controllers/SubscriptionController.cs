using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IMediator _mediator;
    
    public SubscriptionController(IServiceManager serviceManager, IMediator mediator)
    {
        _serviceManager = serviceManager;
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<List<Subscription>> GetAll()
    {
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = (await _mediator.Send(query)).Value;
        return subscriptions!.ToList();
    }
}