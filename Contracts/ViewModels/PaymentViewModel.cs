using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class PaymentViewModel
{
    [Required]
    [MinLength(13)]
    [MaxLength(16)]
    public string CardNumber { get; set; } = null!;
    [Required]
    [Range(1,12)]
    public int Month { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Code { get; set; }

}