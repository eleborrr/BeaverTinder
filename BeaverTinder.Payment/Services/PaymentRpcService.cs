using BeaverTinder.Payment.Core.Dto.Payment;
using BeaverTinder.Payment.Infrastructure.Persistence;
using BeaverTinder.Shared;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BeaverTinder.Payment.Services;

public class PaymentRpcService: BeaverTinder.Shared.Payment.PaymentBase
{
    private readonly PaymentDbContext _dbContext;

    public PaymentRpcService(PaymentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<PaymentResponse> Add(PaymentMsg paymentRequest, ServerCallContext context)
    {
        Console.WriteLine("Trying to add payment");
        await Task.Delay(2000);
        if (CheckBillingInfoIsCorrect(paymentRequest.CardNumber, paymentRequest.Month, paymentRequest.Year))
        {
            return new PaymentResponse()
            {
                Successful = false,
                Message = "Billing info is incorrect"
            };
        }

        var payment = new Core.Entities.Payment()
        {
            Amount = paymentRequest.Amount,
            PaymentDate = DateTime.Now,
            SubsId = paymentRequest.SubId,
            UserId = paymentRequest.UserId
        };
        
        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();


        return new PaymentResponse()
        {
            PaymentId = payment.Id,
            Successful = true
        };
    }

    public override async Task<PhaseResponse> Prepare(Empty request, ServerCallContext context)
    {
        await Task.Delay(2000);

        return new PhaseResponse() { Result = true };
    }

    public override async Task<PhaseResponse> Refund(RefundRequest refundRequest, ServerCallContext context)
    {
        await Task.Delay(2000);


        var payment = _dbContext.Payments.FirstOrDefault(payment => payment.Id == refundRequest.PaymentId);

        if (payment is null)
            return new PhaseResponse { Result = true };
        
        _dbContext.Payments.Remove(payment);
        await _dbContext.SaveChangesAsync();

        return new PhaseResponse()
        {
            Result = true
        };
    }


    private static bool CheckBillingInfoIsCorrect(string number, int month, int year)
    {
        var ends = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        if (ends.CompareTo(DateTime.Now.Date) > 0)
            return false;

        var correctProvider = false;
        if (!long.TryParse(number, out _))
            return false;
        if ((number.StartsWith("34") || number.StartsWith("37")) && (number.Length == 15))
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
        for (var i = 0; i < number.Length; i++)
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
