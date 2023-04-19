﻿using Microsoft.AspNetCore.Identity;

namespace Services.Abstraction.TwoFA;

public interface ITwoFAService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
}