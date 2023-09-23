namespace Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public double PricePerMonth { get; set; }
}