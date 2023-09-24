namespace Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double PricePerMonth { get; set; }
}