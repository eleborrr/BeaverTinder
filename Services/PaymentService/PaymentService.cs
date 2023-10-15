using Contracts;
using Contracts.Responses.Payment;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Payments;

namespace Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepositoryManager _repositoryManager;
        public PaymentService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<PaymentDto> ProcessPayment(PaymentRequestDto paymentRequest)
        {
            await Task.Delay(2000);
            if (CheckBillingInfoIsCorrect(paymentRequest.CardNumber, paymentRequest.Month, paymentRequest.Year))
            {
                return new PaymentDto()
                {
                    StatusCode = PaymentResponseStatus.InvalidData
                };
            }
            var payment = new PaymentDto()
            {
                Amount = paymentRequest.Amount,
                PaymentDate = DateTime.Now,
                StatusCode = PaymentResponseStatus.Ok,
                SubsId = paymentRequest.SubsId,
                UserId = paymentRequest.UserId
            };

            await _repositoryManager.PaymentRepository.AddAsync(new Payment()
            {
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                SubsId = payment.SubsId,
                UserId = payment.UserId
            });
            
            return payment;
        }

        private static bool CheckBillingInfoIsCorrect(string number, int month, int year)
        {
            var ends = new DateTime(year, month, 1);
            if (ends.CompareTo(DateTime.Now.Date) > 0)
                return false;
            var sum = 0;
            var correctProvider = false;
            if (!long.TryParse(number, out _))
                return false;
            if ((number.StartsWith("34") || number.StartsWith("37")) && (number.Length == 15))
                correctProvider = true;
            else if ((number.StartsWith("51")) || (number.StartsWith("52")) ||
                     (number.StartsWith("53")) || (number.StartsWith("54")) ||
                     (number.StartsWith("55")) && (number.Length == 16))
                correctProvider = true;
            else if ((number.StartsWith("4")) && number.Length is 13 or 16)
                correctProvider = true;
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
            return sum % 10 == 0 && correctProvider;
        }
    }
}
