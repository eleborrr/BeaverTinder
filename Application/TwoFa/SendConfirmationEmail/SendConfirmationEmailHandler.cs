using System.Text;
using Contracts.Dto.MediatR;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Services.Abstraction.Cqrs.Queries;
using Services.Abstraction.Email;

namespace Application.TwoFa.SendConfirmationEmail;

public class SendConfirmationEmailHandler : IQueryHandler<SendConfirmationEmailQuery, Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public SendConfirmationEmailHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result<Unit>> Handle(SendConfirmationEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                    return new Result<Unit>(new Unit(), false, "Can not find user by id");
                
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                
                var link = $"https://localhost:7015/confirm?userEmail={user.Email}&token={codeEncoded}";
                
                await _emailService.SendEmailAsync(user.Email!, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{link}\">ссылка</a>");
                return new Result<Unit>(new Unit(), true, null);
    }
}