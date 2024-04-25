using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace BeaverTinder.Shared.Dto.Payment;

public class PaymentRequestDto
{
    public string UserId { get; set; } = null!;

    [Required]
    [MinLength(13)]
    [MaxLength(16)]
    public string CardNumber { get; set; } = null!;

    [Required] [Range(1, 12)] public int Month { get; [UsedImplicitly] init; } 
    [Range(0, double.MaxValue)] public double Amount { get; [UsedImplicitly] init; } 
    [Required] public int Year { get; [UsedImplicitly] init; } 

    [Required]
    [MinLength(3)]
    [MaxLength(3)]
    public string Code { get; set; } = null!;

    public int SubsId { get; [UsedImplicitly] init; } 
}