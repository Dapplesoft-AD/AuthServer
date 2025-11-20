using FluentValidation;

namespace Application.Permissions.Delete;

public class DeletePermissionValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Permission ID is required.");
    }
}
