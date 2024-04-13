using System.Security.Claims;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Grpc.Net.Client;
using BeaverTinder.Shared;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.AddSubscription;

public class AddSubscriptionHandler: ICommandHandler<AddSubscriptionCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly BeaverTinder.Shared.Subscription.SubscriptionClient _subscriptionClient;

    public AddSubscriptionHandler(UserManager<User> userManager, IRepositoryManager repositoryManager, BeaverTinder.Shared.Subscription.SubscriptionClient subscriptionClient)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _subscriptionClient = subscriptionClient;
    }

    public async Task<Result> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("In add sub handler");
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return new Result(false, "User not found");
        // var channel = GrpcChannel.ForAddress("dns:///subscription:8083");
        // var _subscriptionClient = new grpcServices.Subscription.SubscriptionClient(channel);
        var updateSubResponse = await _subscriptionClient.AddUserSubscriptionAsync(new UpdateSubscriptionMsg()
        {
            SubscriptionId = request.SubscriptionId,
            UserId = request.UserId
        }, cancellationToken: cancellationToken);

        if (updateSubResponse.Result)
        {
            await _userManager.AddToRoleAsync(user, updateSubResponse.RoleName);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, updateSubResponse.RoleName));
            return new Result(true);
        }
        return new Result(false, updateSubResponse.Message);
    }
}