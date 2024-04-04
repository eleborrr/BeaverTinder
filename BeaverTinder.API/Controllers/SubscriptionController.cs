using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : Controller
{
    private readonly IMediator _mediator;
    
    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<List<SubscriptionInfoDto>> GetAll()
    {
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = (await _mediator.Send(query)).Value;
        return subscriptions!.ToList();
    }
}