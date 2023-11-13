using Application.FindBeaver.GetNextBeaver;
using FluentValidation;

namespace Application.FindBeaver.GetNextSympathy;

public class GetNextSympathyValidator : AbstractValidator<GetNextSympathyQuery>
{
    public GetNextSympathyValidator()
    {
        RuleFor(q => q.CurrentUser).NotNull();
    }
}