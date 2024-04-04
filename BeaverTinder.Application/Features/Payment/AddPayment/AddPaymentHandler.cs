using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Shared.Dto.Payment;
using grpcServices;

namespace BeaverTinder.Application.Features.Payment.AddPayment;

public class AddPaymentHandler : ICommandHandler<AddPaymentCommand, PaymentIdDto>
{
    private readonly grpcServices.Payment.PaymentClient _paymentClient;

    public AddPaymentHandler(grpcServices.Payment.PaymentClient paymentClient)
    {
        _paymentClient = paymentClient;
    }
    
    public async Task<Result<PaymentIdDto>> Handle(
        AddPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        var paymentRes = await _paymentClient.AddAsync(new PaymentMsg
        {
            Amount = request.Amount,
            Year = request.Year,
            Month = request.Month,
            CardNumber = request.CardNumber,
            Code = request.Code,
            SubId = request.SubsId,
            UserId = request.UserId
        }, cancellationToken: cancellationToken);

        if (paymentRes.Successful)
            return new Result<PaymentIdDto>(new PaymentIdDto(paymentRes.PaymentId), true);
        return new Result<PaymentIdDto>(null, false, paymentRes.Message);
    }
}