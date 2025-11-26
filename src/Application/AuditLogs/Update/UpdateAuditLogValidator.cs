
using FluentValidation;

namespace Application.AuditLogs.Update;
public sealed class UpdateAuditLogValidator : AbstractValidator<UpdateAuditLogCommand>
{
    public UpdateAuditLogValidator()
    {
        RuleFor(a => a.AuditLogId)
            .NotEmpty()
            .WithMessage("AuditLogId is required.");

        RuleFor(a => a.Action)
            .NotEmpty()
            .WithMessage("Action is required.")
            .MaximumLength(255)
            .WithMessage("Action cannot exceed 255 characters.");

        RuleFor(a => a.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
    }
}
