using System.Data;
using FluentValidation;

namespace Application.FindBeaver.GetNextBeaver;

public class GetNextBeaverValidator : AbstractValidator<GetNextBeaverQuery>
{
    public GetNextBeaverValidator()
    {
        RuleFor(q => q.CurrentUser).NotNull();

        RuleFor(q => q.UserRole).NotNull();
    }
}