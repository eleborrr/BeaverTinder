using System.Security.Claims;
using Contracts;
using Contracts.Enums;
using Contracts.Responses.Login;
using Contracts.Responses.Registration;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction.Email;
using Services.Abstraction.OAuth;
using static System.Enum;

namespace Services.OAuth;

public class VkOAuthService : IVkOAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public VkOAuthService(IRepositoryManager repositoryManager, UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<LoginResponseDto> AuthAsync(VkAuthDto authDto)
    {
        var vkUser = await _repositoryManager.UserToVkRepository.GetByIdAsync(authDto.VkId);
        if (vkUser is null)
        {
            var regResult = await Register(authDto);
            var createdUser = await _userManager.FindByNameAsync(authDto.UserName);
            var userToVk = new UserToVk()
            {
                UserId = createdUser.Id,
                VkId = authDto.VkId
            };
            await _repositoryManager.UserToVkRepository.AddAsync(userToVk);
            return await Login(createdUser);
        }
        var userId = await _repositoryManager.UserToVkRepository.GetByIdAsync(authDto.VkId);
        var signedUser = await _signInManager.UserManager.FindByIdAsync(userId.UserId);
        return await Login(signedUser);

        // ModelState.AddModelError("error_message", "Invalid login attempt.");
    }

    public async Task<RegisterResponseDto> Register(VkAuthDto authDto)
    {
        Gender gender;
        TryParse(authDto.Gender, out gender);
        var user = new User
        {
            LastName = authDto.LastName,
            FirstName = authDto.FirstName,
            UserName = authDto.UserName,
            Email = authDto.Email,
            Gender = gender.ToString(),
            About = authDto.About,
            Image = "TEST",
            DateOfBirth = authDto.DateOfBirth
        };
            
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            return new RegisterResponseDto(RegisterResponseStatus.Ok);
            // TODO протестить что норм работает
            // await _geolocationService.AddAsync(userId: _userManager.FindByEmailAsync(user.Email).Id,
            //     Latutide: 55.47, // geolocation from dto!
            //     Longtitude: 49.6);
        }

        return new RegisterResponseDto(RegisterResponseStatus.Fail,
            result.Errors.FirstOrDefault().Description);
    }
    
    public async Task<LoginResponseDto> Login(User signedUser)
    {
        await _signInManager.SignInAsync(signedUser, false);
        await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
        if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
            await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));
        return new LoginResponseDto(LoginResponseStatus.Ok, await _jwtGenerator.GenerateJwtToken(signedUser.Id));
    }
}