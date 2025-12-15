using FluentValidation;

namespace Application.Projects.Update;
public class UpdateProjectQueryValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage("Application ID is required.");

        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(a => a.ClientId)
            .NotEmpty()
            .WithMessage("ClientId is required.")
            .MaximumLength(100)
            .WithMessage("ClientId must not exceed 100 characters.");

        RuleFor(a => a.ClientSecret)
            .NotEmpty()
            .WithMessage("ClientSecret is required.")
            .MaximumLength(255)
            .WithMessage("ClientSecret must not exceed 255 characters.");

        RuleFor(a => a.Domain)
            .NotEmpty()
            .WithMessage("Domain is required.")
            .MaximumLength(255)
            .WithMessage("Domain must not exceed 255 characters.");

        RuleFor(a => a.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid value.");
    }
}
