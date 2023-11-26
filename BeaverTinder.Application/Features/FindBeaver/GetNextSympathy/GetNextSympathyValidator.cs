using Application.FindBeaver.GetNextBeaver;
using BeaverTinder.Application.Features.FindBeaver.GetNextSympathy;
using FluentValidation;

namespace Application.FindBeaver.GetNextSympathy;

public class GetNextSympathyValidator : AbstractValidator<GetNextSympathyQuery>
{
    public GetNextSympathyValidator()
    {
        RuleFor(q => q.CurrentUser).NotNull();
    }
}