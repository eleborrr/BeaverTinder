using System.Security.Claims;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Features.Subscription.AddSubscription;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Shared;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.RemoveUserSubscription;

public class RemoveUserSubscriptionHandler: ICommandHandler<RemoveUserSubscriptionCommand>
{
    private readonly BeaverTinder.Shared.Subscription.SubscriptionClient _subscriptionClient;

    public RemoveUserSubscriptionHandler(BeaverTinder.Shared.Subscription.SubscriptionClient subscriptionClient)
    {
        _subscriptionClient = subscriptionClient;
    }

    public async Task<Result> Handle(RemoveUserSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var refundRes = await _subscriptionClient.RefundUserSubscriptionAsync(new UpdateSubscriptionMsg()
        {
            SubscriptionId = request.subId,
            UserId = request.userId
        });
        if (refundRes.Result)
            return new Result(true);
        return new Result(false, "Error while removing subscription from user");
    }
}