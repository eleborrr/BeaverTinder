using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Dto.Payment;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Payment.AddPayment;

public class AddPaymentHandler : ICommandHandler<AddPaymentCommand, PaymentIdDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddPaymentHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public async Task<Result<PaymentIdDto>> Handle(
        AddPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        if (CheckBillingInfoIsCorrect(request.CardNumber, request.Month, request.Year))
        {
            return new Result<PaymentIdDto>(
                new PaymentIdDto(-1),
                false,
                "Billing info is not correct");
        }
        var payment = new Domain.Entities.Payment
        {
            Amount = request.Amount,
            PaymentDate = DateTime.Now,
            SubsId = request.SubsId,
            UserId = request.UserId
        };

        var id = await _repositoryManager.PaymentRepository.AddAsync(payment);
        return new Result<PaymentIdDto>(new PaymentIdDto(id), true);
    }
    
    private static bool CheckBillingInfoIsCorrect(string number, int month, int year)
    {
        var ends = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        if (ends.CompareTo(DateTime.Now.Date) > 0)
            return false;
        
        var correctProvider = false;
        if (!long.TryParse(number, out _))
            return false;
        if ((number.StartsWith("34") || number.StartsWith("37")) && number.Length == 15)
            correctProvider = true;
        else if (number.StartsWith("51") || number.StartsWith("52") ||
                 number.StartsWith("53") || number.StartsWith("54") ||
                 number.StartsWith("55") && number.Length == 16)
            correctProvider = true;
        else if (number.StartsWith('4') && number.Length is 13 or 16)
            correctProvider = true;
        
        return GetCheckSum(number) % 10 == 0 && correctProvider;
    }

    private static int GetCheckSum(string number)
    {
        var sum = 0;
        for (var i = 0; i < number.Length; i ++)
        {
            if (i % 2 == 0)
            {
                sum += int.Parse(number[i].ToString());
            }
            else
            {
                var temp = 2 * int.Parse(number[i].ToString());
                if (temp > 9)
                    sum += temp - 9;
                else
                    sum += temp;
            }
        }

        return sum;
    }
}