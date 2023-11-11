using Contracts.Dto.Account;
using Contracts.Dto.Authentication.Login;
using Contracts.Dto.Authentication.Register;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services.Abstraction.Account;

public interface IAccountService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string? userEmail, string? token);
    public Task<LoginResponseDto> Login(LoginRequestDto model, ModelStateDictionary modelState);

    public Task<RegisterResponseDto> Register(RegisterRequestDto model, ModelStateDictionary modelState);
    public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
    public Task<EditUserResponseDto> EditAccount(User currentUser, EditUserRequestDto model, ModelStateDictionary modelState);
    public Task<IEnumerable<User>> GetAllMappedUsers();
}