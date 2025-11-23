using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Roles.Create;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(r => r.RoleName)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MaximumLength(100)
            .WithMessage("Role name must not exceed 100 characters.")
            .Matches("^[a-zA-Z][a-zA-Z0-9_ ]*$")
            .WithMessage("Role name must start with a letter and can contain letters, numbers, spaces, and underscores.");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
    }
}
