using FluentValidation;

namespace Application.MfaSettings.Delete;

public sealed class DeleteMfaSettingCommandValidator
    : AbstractValidator<DeleteMfaSettingCommand>
{
    public DeleteMfaSettingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("MfaSetting Id is required.");
    }
}
