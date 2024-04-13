using System.Collections.Concurrent;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Features.Payment.AddPayment;
using BeaverTinder.Application.Features.Subscription.AddSubscription;
using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Features.Subscription.RemoveUserSubscription;
using BeaverTinder.Application.Services.Abstractions.TransactionManager;
using BeaverTinder.Domain.Enums;
using BeaverTinder.Domain.Models;
using BeaverTinder.Shared.Dto.Payment;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Application.Services.TransactionManager;

public class TransactionManager : ITransactionManager
{
    private readonly IMediator _mediator;
    private readonly ConcurrentDictionary<string, ReadyType> _states = new();

    public TransactionManager(IMediator mediator)
    {
        _states.TryAdd("Payment", ReadyType.Ready);
        _states.TryAdd("Subscription", ReadyType.Ready);
        _mediator = mediator;
    }

    public async Task<Result> PrepareServicesAsync(PaymentRequestDto model)
    {
        Console.WriteLine("Trying to prepare services");
        var subscriptions= await _mediator.Send(new GetUsersActiveSubscriptionQuery(model.UserId));
        Console.WriteLine(subscriptions.IsSuccess);
        Console.WriteLine(subscriptions.Value.Id);
        Console.WriteLine(model.SubsId);
        if (subscriptions.IsSuccess && subscriptions.Value.Id != model.SubsId)
        {
            Console.WriteLine("No active sub with such id found");
            foreach (var stateKeyVal in _states)
            {
                _states.TryUpdate(stateKeyVal.Key, ReadyType.Pending, stateKeyVal.Value);
            }

            return new Result(true);
        }
        Console.WriteLine("Found active sub with such id");
        return new Result(false, "Such subscription already exits");
    }

    public bool CheckReadyServicesAsync()
    {
        Console.WriteLine("Checking are services ready");
        Console.WriteLine(_states["Payment"]);
        Console.WriteLine(_states["Subscription"]);
        return _states.All(keyValPair => keyValPair.Value == ReadyType.Ready);
    }

    public async Task<Result> CommitAsync(PaymentRequestDto model)
    {
        Console.WriteLine("Trying to commit");
        foreach (var stateKeyVal in _states)
        {
            try
            {
                var resp = new Result(false, "");
                switch (stateKeyVal.Key)
                {
                    case "Payment":
                        resp = await _mediator.Send(
                            new AddPaymentCommand(model.UserId, model.CardNumber, model.Month,
                                model.Amount, model.Year, model.Code, model.SubsId));
                        break;
                    case "Subscription":
                        resp = await _mediator.Send(
                            new AddSubscriptionCommand(model.SubsId, model.UserId));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stateKeyVal.Key));
                }
                _states.TryUpdate(stateKeyVal.Key, resp.IsSuccess ? ReadyType.Pending : ReadyType.Unready, stateKeyVal.Value);
        
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _states.TryUpdate(stateKeyVal.Key, ReadyType.Unready, stateKeyVal.Value);
                return new Result(false, e.Message);
            }
        }

        return new Result(true);
    }

    public async Task RollbackAsync(PaymentRequestDto request)
    {
        Console.WriteLine("Trying to rollback");
        await _mediator.Send(new RemoveUserSubscriptionCommand(request.UserId, request.SubsId));
        
        foreach (var stateKeyVal in _states)
        {
            _states.TryUpdate(stateKeyVal.Key, ReadyType.Ready, stateKeyVal.Value);
        }
    }
}