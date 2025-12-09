using FluentValidation;

namespace Application.Districts.Delete;

public sealed class DeleteDistrictCommandValidator
    : AbstractValidator<DeleteDistrictCommand>
{
    public DeleteDistrictCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("District Id is required.");
    }
}
