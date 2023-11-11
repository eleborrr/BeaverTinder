using System.Security.Claims;
using System.Text;
using Contracts.Dto.Account;
using Contracts.Dto.Authentication.Login;
using Contracts.Dto.Authentication.Register;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction.Account;
using Services.Abstraction.Email;
using Services.Abstraction.Geolocation;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IGeolocationService _geolocationService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountService(UserManager<User> userManager, IEmailService emailService, SignInManager<User> signInManager, IJwtGenerator jwtGenerator, 
        IGeolocationService geolocationService, IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _geolocationService = geolocationService;
        _passwordHasher = passwordHasher;
    }

    public async Task SendConfirmationEmailAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return;

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

        var link = $"https://localhost:7015/confirm?userEmail={user.Email}&token={codeEncoded}";

        await _emailService.SendEmailAsync(user.Email!, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{link}\">ссылка</a>");
    }

    public async Task SendPasswordResetAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

        var link = $"https://localhost:7015/confirm?userEmail={user.Email}&token={codeEncoded}";

        await _emailService.SendEmailAsync(user.Email!, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{link}\">ссылка</a>");

    }

    public async Task<IdentityResult> ConfirmEmailAsync(string? userEmail, string? token)
    {
        if (userEmail == null || token == null)
            return IdentityResult.Failed();
        var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
        var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            return IdentityResult.Failed();

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

    public async Task<LoginResponseDto> Login(LoginRequestDto model, ModelStateDictionary modelState)
    {
        /*bool rememberMe = false;
        /*if (Request.Form.ContainsKey("RememberMe"))
        {
            bool.TryParse(Request.Form["RememberMe"], out rememberMe);
        }#1#
        model.RememberMe = rememberMe;*/

        if (!modelState.IsValid) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        var signedUser = await _signInManager.UserManager.FindByNameAsync(model.UserName);
        if (signedUser is null) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        var result = await _signInManager.PasswordSignInAsync(signedUser.UserName!, model.Password, false,
            lockoutOnFailure: false);


        if (!result.Succeeded) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        try
        {
            await _userManager.RemoveClaimAsync(signedUser, new Claim("Id", signedUser.Id));
            await _userManager.RemoveClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));
            await _userManager.RemoveClaimAsync(signedUser, new Claim(ClaimTypes.Role, "User"));
            await _userManager.RemoveClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Moderator"));
        }
        catch (Exception)
        {
            // ignored
        }
                
        await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
        if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
        {
                    
            await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));
        }

        else if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Moderator"))
            await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Moderator"));

        else
            await _userManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "StandartUser"));

        return new LoginResponseDto(LoginResponseStatus.Ok, await _jwtGenerator.GenerateJwtToken(signedUser.Id));

    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto model, ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) return new RegisterResponseDto(RegisterResponseStatus.InvalidData);
        var user = new User
        {
            LastName = model.LastName,
            FirstName = model.FirstName,
            UserName = model.UserName,
            Email = model.Email,
            Gender = model.Gender,
            About = model.About,
            DateOfBirth = model.DateOfBirth,
            Image = "https://w7.pngwing.com/pngs/831/88/png-transparent-user-profile-computer-icons-user-interface-mystique-miscellaneous-user-interface-design-smile.png",

        };

        var emailCollision = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);
        if (emailCollision is not null)
            return new RegisterResponseDto(RegisterResponseStatus.Fail, "User with that email already exists");
            
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new RegisterResponseDto(RegisterResponseStatus.Fail,
                result.Errors.FirstOrDefault()!.Description);
        await SendConfirmationEmailAsync(user.Id);
                
        var userInDb = await _userManager.FindByEmailAsync(user.Email);
        if (userInDb is null)
            return new RegisterResponseDto(RegisterResponseStatus.Fail, "User registration error");
                
        await _userManager.AddClaimAsync(userInDb, new Claim(ClaimTypes.Role, "User"));
        await _geolocationService.AddAsync(userId:userInDb.Id,
            latitude: model.Latitude,
            longitude: model.Longitude);
        return new RegisterResponseDto(RegisterResponseStatus.Ok);
    }

    public async Task<EditUserResponseDto> EditAccount(User userToEdit, EditUserRequestDto model, ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) return new EditUserResponseDto(EditUserResponseStatus.InvalidData);
        
        var passwordHash = model.Password == "" ? userToEdit.PasswordHash : _passwordHasher.HashPassword(userToEdit, model.Password);
            

        userToEdit.LastName = model.LastName;
        userToEdit.FirstName = model.FirstName;
        userToEdit.UserName = model.UserName;
        userToEdit.Gender = model.Gender;
        userToEdit.About = model.About;
        userToEdit.Image = model.Image;
        userToEdit.PasswordHash = passwordHash;
            
            
        var result = await _userManager.UpdateAsync(userToEdit);

        await _geolocationService.Update(userToEdit.Id, model.Latitude, model.Longitude);

        if (!result.Succeeded)
            return new EditUserResponseDto(EditUserResponseStatus.Fail,
                result.Errors.FirstOrDefault()!.Description);
            
        return new EditUserResponseDto(EditUserResponseStatus.Ok);
    }
   
    public async Task<IEnumerable<User>> GetAllMappedUsers()
    {
        return await _userManager.Users.ToListAsync();
    }
}