using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class PaymentRequestDto
{
    public string UserId { get; set; }
    [Required]
    [MinLength(13)]
    [MaxLength(16)]
    public string CardNumber { get; set; }
    [Required]
    [Range(1,12)]
    public int Month { get; set; }
    [Range(0, double.MaxValue)]
    public double Amount { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(3)]
    public string Code { get; set; }
    public int SubsId { get; set; }
}