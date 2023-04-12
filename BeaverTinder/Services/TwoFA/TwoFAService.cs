using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using BeaverTinder.Models;
using DogApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace BeaverTinder.Services;

public class TwoFAService: ITwoFAService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailServiceInterface _emailService;

    public TwoFAService(UserManager<User> userManager, IEmailServiceInterface emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task SendConfirmationEmailAsync(User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        
        var link = $"https://localhost:7015/confirm?userEmail={user.Email}&token={codeEncoded}";
        
        await _emailService.SendEmailAsync(user.Email, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{link}\">ссылка</a>");
        
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token)
    {
        var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
        var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return IdentityResult.Failed();
        }
        var res = await _userManager.ConfirmEmailAsync(user, codeDecoded);
        return res;
    }
}