using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Email;
using Services.Abstraction.TwoFA;

namespace Services.Abstraction;

public interface IServiceManager
{
    IEmailService EmailService { get; }
    ITwoFAService TwoFaService { get; }
}