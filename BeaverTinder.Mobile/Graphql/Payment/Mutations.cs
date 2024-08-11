using BeaverTinder.Application.Services.Abstractions.TransactionManager;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Shared.Dto.Payment;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    [Authorize]
    public async Task<Result> Pay(
        [FromBody] PaymentRequestDto model,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var id = context.User.FindFirstValue("id")!;
        model.UserId = id;
        var transactionManager = scope.ServiceProvider.GetRequiredService<ITransactionManager>();
        var isServicesReady = transactionManager.CheckReadyServicesAsync();
        var transactionState = new Result(false, "Services in pending state");
        if (isServicesReady)
        {
            var prepared = await transactionManager.PrepareServicesAsync(model);
            if (prepared.IsSuccess)
            {
                transactionState = await transactionManager.CommitAsync(model);
            }

            if (!transactionState.IsSuccess)
            {
                await transactionManager.RollbackAsync(model);
                return transactionState;
            }

            return transactionState;
        }
        Console.WriteLine("services not ready");

        return transactionState;
    }
}