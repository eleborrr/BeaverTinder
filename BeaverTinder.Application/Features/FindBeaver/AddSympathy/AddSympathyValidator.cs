using BeaverTinder.Application.Features.FindBeaver.AddSympathy;
using FluentValidation;

namespace Application.FindBeaver.AddSympathy;

public class AddSympathyValidator : AbstractValidator<AddSympathyCommand>
{
    public AddSympathyValidator()
    {
        RuleFor(c => c.UserRole).NotNull();
        RuleFor(c => c.Sympathy).NotNull();
        RuleFor(c => c.User1).NotNull();
        RuleFor(c => c.UserId2).NotNull();
    }
}