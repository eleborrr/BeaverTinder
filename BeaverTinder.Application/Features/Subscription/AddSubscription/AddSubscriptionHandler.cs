﻿using System.Security.Claims;
using Application.Subscription.AddSubscription;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.AddSubscription;

public class AddSubscriptionHandler: ICommandHandler<AddSubscriptionCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;

    public AddSubscriptionHandler(UserManager<User> userManager, IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var sub = await _repositoryManager.SubscriptionRepository.GetBySubscriptionIdAsync(request.SubscriptionId);
        var userSub =
            await _repositoryManager.UserSubscriptionRepository.GetUserSubscriptionByUserIdAndSubsIdAsync(request.SubscriptionId, request.UserId);
        if (userSub == null)
        {
            await _repositoryManager.UserSubscriptionRepository.AddUserSubscriptionAsync(request.SubscriptionId, request.UserId);
            await _userManager.AddToRoleAsync(user!, sub!.RoleName);
            await _userManager.AddClaimAsync(user!, new Claim(ClaimTypes.Role, sub.RoleName));
            return new Result(true);
        }
        if (userSub.Active)
        {
            var exp = userSub.Expires;
            userSub.Expires = exp + TimeSpan.FromDays(30);
            await _repositoryManager.UserSubscriptionRepository.SaveAsync();
            return new Result(true);
        }
        await _repositoryManager.UserSubscriptionRepository.UpdateUserSubAsync(request.SubscriptionId, request.UserId);
        await _userManager.AddToRoleAsync(user!, sub!.RoleName);
        return new Result(true);
    }
}