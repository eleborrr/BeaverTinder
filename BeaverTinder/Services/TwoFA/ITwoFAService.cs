using BeaverTinder.Models;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Services;

public interface ITwoFAService
{
    public Task SendConfirmationEmailAsync(User user);
    public Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
}