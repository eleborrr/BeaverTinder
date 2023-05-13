// using Yandex.Checkout.V3;
// using System.Net;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;
//
// namespace BeaverTinder.Services.Pay;
//
// internal sealed class PaymentService: IPaymentService
// {
//     private string secret_key = "test_izBJTFEOXAuUWTh9720fYGQ9U_FZPnNRBDpvdA0PkTY";
//     private string shop_id = "211284";
//     private AsyncClient _asyncClient;
//
//     public PaymentService()
//     {
//         var client = new Yandex.Checkout.V3.Client(
//             shopId: shop_id,
//             secretKey: secret_key);
//         _asyncClient = client.MakeAsync();
//
//     }
//     
//     public async Task<Payment> DoPayment()
//     {
//         var client = new HttpClient();
//         var newPayment = new NewPayment()
//         {
//             Amount = new Amount { Value = 100.00m, Currency = "RUB" },
//             Confirmation = new Confirmation
//             {
//                 Type = ConfirmationType.Redirect,
//                 ReturnUrl = "https://localhost:7015/"
//             }
//         };
//
//         Payment payment = await _asyncClient.CreatePaymentAsync(newPayment);
//         
//         return payment;
//         // HttpResponse.Redirect();
//         // 3. Дождитесь получения уведомления
//         // Message message = Client.ParseMessage(HttpRequest, Request.ContentType, Request.InputStream);
//         // payment = message?.Object;
//         //
//         // if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
//         // {
//         //     // 4. Подтвердите готовность принять платеж
//         //     _asyncClient.CapturePaymentAsync(payment.Id);
//         // }
//     }
// }

using System.Security.Claims;
using Contracts;
using Contracts.Responses.Payment;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.PaymentService;

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
            if (!long.TryParse(number, out var result))
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
