﻿using Contracts;

namespace Services.Abstraction.Payments;

public interface IPaymentService
{
    public Task<PaymentDto> ProcessPayment(PaymentRequestDto paymentRequest);
}


