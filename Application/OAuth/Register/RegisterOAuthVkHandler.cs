using Contracts.Dto.Authentication.Register;
using Contracts.Dto.MediatR;
using Contracts.Enums;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Cqrs.Commands;
using static System.Enum;

namespace Application.OAuth.Register;

public class RegisterOAuthVkHandler : ICommandHandler<RegisterOAuthVkCommand, RegisterResponseDto>
{
    private readonly UserManager<User> _userManager;

    public RegisterOAuthVkHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<RegisterResponseDto>> Handle(RegisterOAuthVkCommand request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;
        if (!TryParse(userDto.Gender, out Gender gender))
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
            return new Result<RegisterResponseDto>(new RegisterResponseDto(
                RegisterResponseStatus.Fail, 
                "User with that email already exists"),
                false,
                "User with that email already exists");

            
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            return new Result<RegisterResponseDto>(
                new RegisterResponseDto(RegisterResponseStatus.Ok),
                true);
        }

        return new Result<RegisterResponseDto>(new RegisterResponseDto(
            RegisterResponseStatus.Fail,
            result.Errors.FirstOrDefault()!.Description),
            false,
            result.Errors.FirstOrDefault()!.Description);
    }
}