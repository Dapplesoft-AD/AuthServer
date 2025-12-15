using System.Threading.Tasks;
using FluentValidation;

namespace Application.Projects.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(150)
            .WithMessage("Name must not exceed 150 characters.");

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

        RuleFor(a => a.Status)
            .IsInEnum()
            .WithMessage("Status must be either Active or Inactive.");
    }
}
