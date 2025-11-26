using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.AuditLogs.Create;
public class CreateAuditLogValidator : AbstractValidator<CreateAuditLogCommand>
{
    public CreateAuditLogValidator()
    {
        RuleFor(a => a.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(a => a.BusinessId)
            .NotEmpty()
            .WithMessage("BusinessId is required.");

        RuleFor(a => a.Action)
            .NotEmpty()
            .WithMessage("Action is required.")
            .MaximumLength(255)
            .WithMessage("Action cannot exceed 255 characters.");

        RuleFor(a => a.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(a => a.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt is required.");
    }
}
