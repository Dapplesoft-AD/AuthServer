using FluentValidation;

namespace Application.RolePermissions.Update;

public class UpdateRolePermissionValidator : AbstractValidator<UpdateRolePermissionCommand>
{
    public UpdateRolePermissionValidator()
    {
        RuleFor(rp => rp.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(rp => rp.PermissionId)
            .NotEmpty()
            .WithMessage("Current Permission ID is required.");

        RuleFor(rp => rp.NewPermissionId)
            .NotEmpty()
            .WithMessage("New Permission ID is required.")
            .NotEqual(rp => rp.PermissionId)
            .WithMessage("New permission must be different from current permission.");
    }
}
