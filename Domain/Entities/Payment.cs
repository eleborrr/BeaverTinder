namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int SubsId { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}