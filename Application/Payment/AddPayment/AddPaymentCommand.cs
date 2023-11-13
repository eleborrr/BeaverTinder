using Contracts.Dto.Payment;
using JetBrains.Annotations;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Payment.AddPayment;

public class AddPaymentCommand : ICommand<PaymentIdDto>
{
    public string UserId { get; set; } = null!;

    /*[Required]
    [MinLength(13)]
    [MaxLength(16)]*/
    public string CardNumber { get; set; } = null!;

    /*[Required] [Range(1, 12)] */
    public int Month { get; [UsedImplicitly] init; } 
    /*[Range(0, double.MaxValue)] */
    public double Amount { get; [UsedImplicitly] init; } 
    /*[Required] */
    public int Year { get; [UsedImplicitly] init; } 

    /*[Required]
    [MinLength(3)]
    [MaxLength(3)]*/
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