using System.Security.Claims;
using Contracts.Dto.Authentication.Login;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAth.Login;

public class LoginOAuthVkHandler : ICommandHandler<LoginOAuthVkCommand, LoginResponseDto>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginOAuthVkHandler(SignInManager<User> signInManager, UserManager<User> userManager, IJwtGenerator jwtGenerator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginOAuthVkCommand request, CancellationToken cancellationToken)
    {
        var signedUser = request.SignedUser;
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

        return new Result<LoginResponseDto>(new LoginResponseDto(
                LoginResponseStatus.Ok,
                await _jwtGenerator.GenerateJwtToken(signedUser.Id)),
            true);
    }
}