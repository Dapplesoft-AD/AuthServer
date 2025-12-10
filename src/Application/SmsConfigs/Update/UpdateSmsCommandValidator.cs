using FluentValidation;

namespace Application.SmsConfigs.Update;

internal sealed class UpdateSmsCommandValidator : AbstractValidator<UpdateSmsCommand>
{
    public UpdateSmsCommandValidator()
    {
        RuleFor(c => c.SmsId)
            .NotEmpty().WithMessage("SMS ID is required.");
        RuleFor(c => c.SmsToken)
            .NotEmpty().WithMessage("SMS token is required.");
    }
}
