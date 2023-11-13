using BeaverTinder.Application.Features.Like.GetIsMutualSympathy;
using FluentValidation;

namespace Application.Like.GetIsMutualSympathy;

public class GetIsMutualSympathyValidator : AbstractValidator<GetIsMutualSympathyQuery>
{
    public GetIsMutualSympathyValidator()
    {
        RuleFor(c => c.user1).NotNull();
        RuleFor(c => c.user2).NotNull();
    }
    
}