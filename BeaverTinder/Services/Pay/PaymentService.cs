using Yandex.Checkout.V3;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Services.Pay;

public class PaymentService: IPaymentService
{
    private string secret_key = "test_izBJTFEOXAuUWTh9720fYGQ9U_FZPnNRBDpvdA0PkTY";
    private string shop_id = "211284";
    private AsyncClient _asyncClient;

    public PaymentService()
    {
        var client = new Yandex.Checkout.V3.Client(
            shopId: shop_id,
            secretKey: secret_key);
        _asyncClient = client.MakeAsync();

    }
    
    public async Task<Payment> DoPayment()
    {
        var client = new HttpClient();
        var newPayment = new NewPayment()
        {
            Amount = new Amount { Value = 100.00m, Currency = "RUB" },
            Confirmation = new Confirmation
            {
                Type = ConfirmationType.Redirect,
                ReturnUrl = "https://localhost:7015/"
            }
        };

        Payment payment = await _asyncClient.CreatePaymentAsync(newPayment);
        
        return payment;
        // HttpResponse.Redirect();
        // 3. Дождитесь получения уведомления
        // Message message = Client.ParseMessage(HttpRequest, Request.ContentType, Request.InputStream);
        // payment = message?.Object;
        //
        // if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
        // {
        //     // 4. Подтвердите готовность принять платеж
        //     _asyncClient.CapturePaymentAsync(payment.Id);
        // }
    }
}