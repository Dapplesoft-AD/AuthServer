using FluentValidation;

namespace Application.Projects.Delete;
public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage("Application ID is required.");
    }
}
