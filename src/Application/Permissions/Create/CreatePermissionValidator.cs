using FluentValidation;

namespace Application.Permissions.Create;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionValidator()
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage("Permission code is required.")
            .MaximumLength(100)
            .WithMessage("Permission code must not exceed 100 characters.")
            .Matches("^[A-Z]([A-Z_]*[A-Z])?$")
            .WithMessage("Permission code must be in uppercase with underscores (e.g., 'CAN_VIEW_REPORT', 'CAN_DELETE_USER').");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
    }
}
