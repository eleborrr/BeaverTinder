using BeaverTinder.Application.Dto.Payment;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using JetBrains.Annotations;

namespace Application.Payment.AddPayment;

public class AddPaymentCommand : ICommand<PaymentIdDto>
{
    public string UserId { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public int Month { get; [UsedImplicitly] init; }
    public double Amount { get; [UsedImplicitly] init; }
    public int Year { get; [UsedImplicitly] init; }
    public string Code { get; set; } = null!;

    public int SubsId { get; [UsedImplicitly] init; }

    public AddPaymentCommand(string userId, string cardNumber, int month, double amount, int year, string code, int subsId)
    {
        UserId = userId;
        CardNumber = cardNumber;
        Month = month;
        Amount = amount;
        Year = year;
        Code = code;
        SubsId = subsId;
    }
}