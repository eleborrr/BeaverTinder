using Contracts.Responses.Payment;

namespace Contracts;

public class PaymentDto
{
    public string UserId { get; set; } = null!;
    public int SubsId { get; set; }
    public DateTime PaymentDate { get; set; }
    public double Amount { get; set; }
    public PaymentResponseStatus StatusCode { get; set; }
}