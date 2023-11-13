using FluentValidation;

namespace Application.OAth.GetVkUserInfo;

public class GetVkUserInfoValidator : AbstractValidator<GetVkUserInfoQuery>
{
    public GetVkUserInfoValidator()
    {
        RuleFor(q => q.AccessToken).NotNull();
    }
}