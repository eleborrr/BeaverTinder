﻿using System.Globalization;
using System.Security.Claims;
using AspNet.Security.OAuth.Vkontakte;
using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Helpers.JwtGenerator;
using BeaverTinder.Application.Services.Abstractions.Geolocation;
using BeaverTinder.Application.Services.Abstractions.OAuth;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Enums;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using static System.Enum;
using JsonSerializer = System.Text.Json.JsonSerializer;
using RegisterResponseDto = BeaverTinder.Application.Dto.Authentication.Register.RegisterResponseDto;

namespace BeaverTinder.Application.Services.OAuth;

public class VkOAuthService : IVkOAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly HttpClient _client;
    private readonly IGeolocationService _geolocationService;

    public VkOAuthService(
        IRepositoryManager repositoryManager,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator,
        HttpClient client,
        IGeolocationService geolocationService)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _client = client;
        _geolocationService = geolocationService;
    }

    public async Task<LoginResponseDto> AuthAsync(VkAuthDto userDto)
    {
        var vkUser = await _repositoryManager.UserToVkRepository.GetByIdAsync(userDto.VkId);
        if (vkUser is null)
        {
            var regResult = await Register(userDto);
            if (!regResult.Successful)
                return new LoginResponseDto(LoginResponseStatus.Fail);
            var createdUser = await _userManager.FindByNameAsync(userDto.UserName);
            if(createdUser == null)
                return new LoginResponseDto(LoginResponseStatus.Fail);
            var userToVk = new UserToVk
            {
                UserId = createdUser.Id,
                VkId = userDto.VkId
            };
            await _repositoryManager.UserToVkRepository.AddAsync(userToVk);
            return await Login(createdUser);
        }
        var userVk = await _repositoryManager.UserToVkRepository.GetByIdAsync(userDto.VkId);
        var signedUser = await _signInManager.UserManager.FindByIdAsync(userVk!.UserId);
        return await Login(signedUser!);
    }

    public async Task<RegisterResponseDto> Register(VkAuthDto userDto)
    {
        if (TryParse(userDto.Gender, out Gender gender))
            gender = Gender.Undefined;
        
        var user = new User
        {
            LastName = userDto.LastName,
            FirstName = userDto.FirstName,
            UserName = userDto.UserName,
            Email = userDto.Email,
            Gender = gender.ToString(),
            About = userDto.About,
            Image = userDto.PhotoUrl,
            DateOfBirth = userDto.DateOfBirth,
        };
        
        var emailCollision = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);
        if (emailCollision is not null)
            return new RegisterResponseDto(
                RegisterResponseStatus.Fail,
                "User with that email already exists");

            
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            var userDb = await _userManager.FindByIdAsync(user.Id);
            await _userManager.AddClaimAsync(userDb!, new Claim(ClaimTypes.Role, "User"));
            await _geolocationService.AddAsync(userId:userDb!.Id,
                latitude: 55.558741,
                longitude: 37.378847);
            return new RegisterResponseDto(RegisterResponseStatus.Ok);
        }

        return new RegisterResponseDto(RegisterResponseStatus.Fail,
            result.Errors.FirstOrDefault()!.Description);
    }
    
    public async Task<LoginResponseDto> Login(User signedUser)
    {
        await _signInManager.SignInAsync(signedUser, false);
        await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
        
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
    
    public async Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto)
    {
        if (!DateTime.TryParse(vkUserDto.DateOfBirth, CultureInfo.CurrentUICulture, out var parsedDate))
        {
            parsedDate = DateTime.Parse("2.1.1999", CultureInfo.CurrentUICulture);
        }

        var registerDto = new VkAuthDto
        {
            VkId = vkUserDto.VkId.ToString(),
            About = vkUserDto.About,
            Email = vkUserDto.Email,
            FirstName = vkUserDto.FirstName,
            Gender = vkUserDto.Gender.ToString(),
            LastName = vkUserDto.LastName,
            UserName = vkUserDto.UserName,
            DateOfBirth = parsedDate,
            PhotoUrl = vkUserDto.PhotoUrl
        };
        var authRes = await AuthAsync(registerDto);
        return authRes;
    }
    
    public async Task<VkUserDto?> GetVkUserInfoAsync(VkAccessTokenDto accessToken)
    {
        var query = new Dictionary<string, string?>
        {
            ["fields"] = "screen_name, bdate, sex, status, about, photo_max_orig",
            ["access_token"] = accessToken.Token,
            ["v"] = "5.131",
        };
        var uri = QueryHelpers
            .AddQueryString(VkontakteAuthenticationDefaults.UserInformationEndpoint, query);
        var userInfo = await _client.GetAsync(uri);
        var content = await userInfo.Content.ReadAsStringAsync();
        var resp = JsonSerializer.Deserialize<VkResponseDto>(content);
        var user = resp!.Response!.FirstOrDefault();
        user!.Email = accessToken.Email;
        return user;
    }
}