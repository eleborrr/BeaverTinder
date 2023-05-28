using System.Security.Claims;
using AspNet.Security.OAuth.Vkontakte;
using Contracts;
using Contracts.Enums;
using Contracts.Responses.Login;
using Contracts.Responses.Registration;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction.Email;
using Services.Abstraction.OAuth;
using static System.Enum;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Services.OAuth;

public class VkOAuthService : IVkOAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly HttpClient _client;

    public VkOAuthService(IRepositoryManager repositoryManager, UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator, HttpClient client)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _client = client;
    }

    public async Task<LoginResponseDto> AuthAsync(VkAuthDto userDto)
    {
        var vkUser = await _repositoryManager.UserToVkRepository.GetByIdAsync(userDto.VkId);
        if (vkUser is null)
        {
            var regResult = await Register(userDto);
            var createdUser = await _userManager.FindByNameAsync(userDto.UserName);
            var userToVk = new UserToVk()
            {
                UserId = createdUser.Id,
                VkId = userDto.VkId
            };
            await _repositoryManager.UserToVkRepository.AddAsync(userToVk);
            return await Login(createdUser);
        }
        var userId = await _repositoryManager.UserToVkRepository.GetByIdAsync(userDto.VkId);
        var signedUser = await _signInManager.UserManager.FindByIdAsync(userId.UserId);
        return await Login(signedUser);

        // ModelState.AddModelError("error_message", "Invalid login attempt.");
    }

    public async Task<RegisterResponseDto> Register(VkAuthDto userDto)
    {
        Gender gender;
        TryParse(userDto.Gender, out gender);
        var user = new User
        {
            LastName = userDto.LastName,
            FirstName = userDto.FirstName,
            UserName = userDto.UserName,
            Email = userDto.Email,
            Gender = gender.ToString(),
            About = userDto.About,
            Image = "TEST",
            DateOfBirth = userDto.DateOfBirth
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
    
    //TODO обработка скрытой даты рождения
    public async Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto)
    {
        DateTime parsedDate;
        if (!DateTime.TryParse(vkUserDto.DateOfBirth, out parsedDate))
        {
            parsedDate = DateTime.Parse("2.1.1999");
        }

        var registerDto = new VkAuthDto()
        {
            VkId = vkUserDto.VkId.ToString(),
            About = vkUserDto.About,
            Email = vkUserDto.Email,
            FirstName = vkUserDto.FirstName,
            Gender = vkUserDto.Gender.ToString(),
            LastName = vkUserDto.LastName,
            UserName = vkUserDto.UserName,
            DateOfBirth = parsedDate
        };
        var authRes = await AuthAsync(registerDto);
        return authRes;
    }
    
    public async Task<VkUserDto?> GetVkUserInfoAsync(VkAccessTokenDto accessToken)
    {
        var query = new Dictionary<string, string>()
        {
            ["fields"] = "screen_name, bdate, sex, status, about",
            ["access_token"] = accessToken.Token,
            ["v"] = "5.131",
        };
        var uri = QueryHelpers.AddQueryString(VkontakteAuthenticationDefaults.UserInformationEndpoint, query);
        var userInfo = await _client.GetAsync(uri);
        var content = await userInfo.Content.ReadAsStringAsync();
        var resp = JsonSerializer.Deserialize<VkResponseDto>(content);
        var user = resp.Response.FirstOrDefault();
        user.Email = accessToken.Email;
        return user;
    }
}