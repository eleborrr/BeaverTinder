using System.Text;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.TwoFa.ConfirmEmail;

public class ConfirmEmailHandler : ICommandHandler<ConfirmEmailCommand, IdentityResult>
{
    private readonly UserManager<User> _userManager;

    public ConfirmEmailHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<IdentityResult>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var codeDecodedBytes = WebEncoders.Base64UrlDecode(request.Token);
        var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
        var user = await _userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            return new Result<IdentityResult>(null, false, "user is null");
        }
        var res = await _userManager.ConfirmEmailAsync(user, codeDecoded);
        return new Result<IdentityResult>(res, true, null);
    }
}