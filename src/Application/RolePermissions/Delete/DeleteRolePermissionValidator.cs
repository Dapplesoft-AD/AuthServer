using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
