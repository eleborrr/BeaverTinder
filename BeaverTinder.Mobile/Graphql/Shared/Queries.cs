using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Queries(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task<string> Logout()
    {
        return await Task.FromResult("logout success");
    }
    
    public async Task<List<SubscriptionInfoDto>> GetAllSubscriptions()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = (await mediator.Send(query)).Value;
        if (subscriptions is null)
            return new List<SubscriptionInfoDto>();
        return subscriptions.ToList();
    }
}