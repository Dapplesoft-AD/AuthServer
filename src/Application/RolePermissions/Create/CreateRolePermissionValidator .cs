using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Application.RolePermissions.Create;

public class CreateRolePermissionValidator : AbstractValidator<CreateRolePermissionCommand>
{
    public CreateRolePermissionValidator()
    {
        RuleFor(rp => rp.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(rp => rp.PermissionId)
            .NotEmpty()
            .WithMessage("Permission ID is required.");
    }
}
