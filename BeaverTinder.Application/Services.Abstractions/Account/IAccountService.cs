using BeaverTinder.Application.Dto.Account;
using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeaverTinder.Application.Services.Abstractions.Account;

public interface IAccountService
{
    public Task SendConfirmationEmailAsync(string userId);
    public Task<IdentityResult> ConfirmEmailAsync(string? userEmail, string? token);
    public Task<LoginResponseDto> Login(LoginRequestDto model, ModelStateDictionary? modelState = default);

    public Task<RegisterResponseDto> Register(RegisterRequestDto model, ModelStateDictionary? modelState = default);
    public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
    public Task<EditUserResponseDto> EditAccount(User currentUser, EditUserRequestDto model, ModelStateDictionary? modelState = default);
    public Task<IEnumerable<User>> GetAllMappedUsers();
}