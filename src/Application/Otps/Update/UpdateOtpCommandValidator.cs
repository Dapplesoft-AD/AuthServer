using FluentValidation;
namespace Application.Otps.Update;

internal sealed class UpdateOtpCommandValidator : AbstractValidator<UpdateOtpCommand>
{
    public UpdateOtpCommandValidator()
    {
        RuleFor(x => x.OtpId)
            .NotEmpty().WithMessage("OTP ID is required.");
    }
}
