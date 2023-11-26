
namespace BeaverTinder.Application.Dto.Payment;

public class PaymentDto
{
    public string UserId { get; init; } = null!;
    public int SubsId { get; init; }
    public DateTime PaymentDate { get; init; }
    public double Amount { get; init; }
    public PaymentResponseStatus StatusCode { get; init; }
}