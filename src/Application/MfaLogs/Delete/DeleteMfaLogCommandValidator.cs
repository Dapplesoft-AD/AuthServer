using FluentValidation;

namespace Application.MfaLogs.Delete;

public sealed class DeleteMfaLogCommandValidator
    : AbstractValidator<DeleteMfaLogCommand>
{
    public DeleteMfaLogCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("MfaLog Id is required.");
    }
}
