using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Cqrs.Commands;

namespace Application.TwoFa.ConfirmEmail;

public class ConfirmEmailCommand: ICommand<IdentityResult>
{
    public string UserEmail { get; set; } 
    public string Token { get; set; }

}