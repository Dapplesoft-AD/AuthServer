using FluentValidation;

namespace Application.Roles.Delete;

public sealed class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("RoleId is required.")
            .NotEqual(Guid.Empty).WithMessage("RoleId cannot be an empty GUID.");
    }
}
