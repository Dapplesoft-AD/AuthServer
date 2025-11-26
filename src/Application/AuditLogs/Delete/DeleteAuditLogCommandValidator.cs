using FluentValidation;

namespace Application.AuditLogs.Delete;

public sealed class DeleteAuditLogCommandValidator
    : AbstractValidator<DeleteAuditLogCommand>
{
    public DeleteAuditLogCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("AuditLog Id is required.");
    }
}
