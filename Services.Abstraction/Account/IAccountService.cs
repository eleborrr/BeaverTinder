﻿using Contracts;
using Contracts.Responses.Login;
using Contracts.Responses.Registration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services.Abstraction.Account;

public interface IAccountService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
    public Task<LoginResponseDto> Login(LoginDto model, ModelStateDictionary modelState);

    public Task<RegisterResponseDto> Register(RegisterDto model, ModelStateDictionary modelState);
    public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
}