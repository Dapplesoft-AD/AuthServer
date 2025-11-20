using FluentValidation;

namespace Application.Permissions.Update;

public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Permission ID is required.");

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage("Permission code is required.")
            .MaximumLength(100)
            .WithMessage("Permission code must not exceed 100 characters.")
            .Matches("^[A-Z][A-Z_]*[A-Z]$")
            .WithMessage("Permission code must be in uppercase with underscores (e.g., 'CAN_VIEW_REPORT', 'CAN_DELETE_USER').");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
    }
}
