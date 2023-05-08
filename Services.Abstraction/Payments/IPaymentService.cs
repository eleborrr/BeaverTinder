using Contracts;

namespace Services.Abstraction.PaymentService;

public interface IPaymentService
{
    public Task<PaymentDto> ProcessPayment(PaymentRequestDto paymentRequest);
}


