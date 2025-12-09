using FluentValidation;

namespace Application.Localities.Delete;

public class DeleteLocalityValidator : AbstractValidator<DeleteLocalityCommand>
{
    public DeleteLocalityValidator()
    {
        RuleFor(x => x.Id)
     .NotEmpty()
     .WithMessage("ID is required.")
     .NotEqual(Guid.Empty)
     .WithMessage("ID cannot be empty GUID.");
    }
}
