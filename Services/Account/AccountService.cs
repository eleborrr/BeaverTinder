using System.Security.Claims;
using System.Text;
using Contracts;
using Contracts.Responses.Login;
using Contracts.Responses.Registration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction;
using Services.Abstraction.Account;
using Services.Abstraction.Email;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public AccountService(UserManager<User> userManager, IEmailService emailService, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task SendConfirmationEmailAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            Console.WriteLine("USER FINDING ERROR");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (token == null)
            Console.WriteLine("TOKEN GENERATE ERROR");

        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

        var link = $"https://localhost:7015/confirm?userEmail={user.Email}&token={codeEncoded}";

        await _emailService.SendEmailAsync(user.Email, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{link}\">ссылка</a>");

    }

    public async Task SendPasswordResetAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            Console.WriteLine("USER FINDING ERROR");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (token == null)
            Console.WriteLine("TOKEN GENERATE ERROR");

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

    public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
        var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
        var user = await _userManager.FindByEmailAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed();
        }

        var res = await _userManager.ResetPasswordAsync(user, codeDecoded, newPassword);
        return res;
    }

    public async Task<LoginResponseDto> Login(LoginDto model, ModelStateDictionary modelstate)
    {
        /*bool rememberMe = false;
        /*if (Request.Form.ContainsKey("RememberMe"))
        {
            bool.TryParse(Request.Form["RememberMe"], out rememberMe);
        }#1#
        model.RememberMe = rememberMe;*/

        if (modelstate.IsValid)
        {
            User? signedUser = await _signInManager.UserManager.FindByNameAsync(model.UserName);
            var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, false,
                lockoutOnFailure: false);


            if (result.Succeeded)
            {
                await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
                if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
                    await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));

                return new LoginResponseDto(LoginResponseStatus.Ok, await _jwtGenerator.GenerateJwtToken(signedUser.Id));
            }

            // ModelState.AddModelError("error_message", "Invalid login attempt.");
        }

        return new LoginResponseDto(LoginResponseStatus.Fail);
    }

    public async Task<RegisterResponseDto> Register(RegisterDto model, ModelStateDictionary modelstate)
    {
        // TODO перенести в сервис
        if (modelstate.IsValid)
        {
            var user = new User
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                About = model.About,
                Image = "TEST",

            };
            //TODO получение геолокации из дто

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SendConfirmationEmailAsync(user.Id);
                return new RegisterResponseDto(RegisterResponseStatus.Ok);
                // TODO протестить что норм работает
                // await _geolocationService.AddAsync(userId: _userManager.FindByEmailAsync(user.Email).Id,
                //     Latutide: 55.47, // geolocation from dto!
                //     Longtitude: 49.6);
            }

            return new RegisterResponseDto(RegisterResponseStatus.Fail,
                result.Errors.FirstOrDefault().Description);
        }
        return new RegisterResponseDto(RegisterResponseStatus.InvalidData);
    }
}