using FluentValidation;

namespace Application.RolePermissions.Delete;

public class DeleteRolePermissionValidator : AbstractValidator<DeleteRolePermissionCommand>
{
    public DeleteRolePermissionValidator()
    {
        RuleFor(rp => rp.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(rp => rp.PermissionId)
            .NotEmpty()
            .WithMessage("Permission ID is required.");
    }
}
