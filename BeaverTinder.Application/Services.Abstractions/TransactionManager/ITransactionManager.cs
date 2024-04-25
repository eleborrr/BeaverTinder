using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Shared.Dto.Payment;

namespace BeaverTinder.Application.Services.Abstractions.TransactionManager;

public interface ITransactionManager
{
    Task<Result> PrepareServicesAsync(PaymentRequestDto model);
    bool CheckReadyServicesAsync();
    Task<Result> CommitAsync(PaymentRequestDto model);
    Task RollbackAsync(PaymentRequestDto request);
}