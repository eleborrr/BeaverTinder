using BeaverTinder.Application.Dto.Payment;

namespace BeaverTinder.Application.Services.Abstractions.Payments;

public interface IPaymentService
{
    public Task<PaymentDto> ProcessPayment(PaymentRequestDto paymentRequest);
}


