using FluentValidation;

namespace Application.Regions.Delete;

public sealed class DeleteRegionCommandValidator
    : AbstractValidator<DeleteRegionCommand>
{
    public DeleteRegionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Region Id is required.");
    }
}
