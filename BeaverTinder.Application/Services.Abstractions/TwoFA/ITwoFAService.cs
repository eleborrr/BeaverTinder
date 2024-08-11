using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Services.Abstractions.TwoFA;

public interface ITwoFaService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
}