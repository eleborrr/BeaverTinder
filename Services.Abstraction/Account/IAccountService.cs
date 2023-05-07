using Contracts;
using Contracts.Responses.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services.Abstraction.Account;

public interface IAccountService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
    public Task<LoginResponseDto> Login(LoginDto model, ModelStateDictionary modelstate);
    
}