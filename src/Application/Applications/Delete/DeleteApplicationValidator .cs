using FluentValidation;

namespace Application.Applications.Delete;
public class DeleteApplicationValidator : AbstractValidator<DeleteApplicationCommand>
{
    public DeleteApplicationValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage("Application ID is required.");
    }
}
