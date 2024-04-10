using System.Collections.Concurrent;
using Application.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Features.Payment.AddPayment;
using BeaverTinder.Application.Features.Subscription.AddSubscription;
using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Application.Features.Subscription.RemoveUserSubscription;
using BeaverTinder.Application.Services.Abstractions.TransactionManager;
using BeaverTinder.Domain.Enums;
using BeaverTinder.Domain.Models;
using BeaverTinder.Shared.Dto.Payment;
using Google.Protobuf.WellKnownTypes;
using grpcServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Application.Services.TransactionManager;

public class TransactionManager : ITransactionManager
{
    private readonly IMediator _mediator;
    private readonly ConcurrentDictionary<string, ReadyType> _states = new();

    public TransactionManager(IMediator mediator)
    {
        _states.AddOrUpdate("Payment", ReadyType.Ready, (key, oldValue) => ReadyType.Ready);
        _states.AddOrUpdate("Subscription", ReadyType.Ready, (key, oldValue) => ReadyType.Ready);
        _mediator = mediator;
    }

    public async Task<Result> PrepareServicesAsync(PaymentRequestDto model)
    {
        var subscriptions= await _mediator.Send(new GetUsersActiveSubscriptionQuery(model.UserId));
        if (subscriptions.IsSuccess && !(subscriptions.Value.Id == model.SubsId))
        {
            foreach (var stateKeyVal in _states)
            {
                _states.TryUpdate(stateKeyVal.Key, ReadyType.Pending, stateKeyVal.Value);
            }

            return new Result(true);
        }
        return new Result(false, "Such subscription already exits");
    }

    public bool CheckReadyServicesAsync()
        => _states.Any(keyValPair => keyValPair.Value != ReadyType.Ready);

    public async Task<Result> CommitAsync(PaymentRequestDto model)
    {
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
                _states.TryUpdate(stateKeyVal.Key, ReadyType.Unready, stateKeyVal.Value);
                return new Result(false, e.Message);
            }
        }

        return new Result(true);
    }

    public async Task RollbackAsync(PaymentRequestDto request)
    {
        await _mediator.Send(new RemoveUserSubscriptionCommand(request.UserId, request.SubsId));
        
        foreach (var stateKeyVal in _states)
        {
            _states.TryUpdate(stateKeyVal.Key, ReadyType.Ready, stateKeyVal.Value);
        }
    }
}