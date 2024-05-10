using System.Web.Http.Results;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Services.Abstractions.TransactionManager;
using BeaverTinder.Shared;
using BeaverTinder.Shared.Dto.Payment;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    public async Task<Result> Pay([FromBody] PaymentRequestDto model, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
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