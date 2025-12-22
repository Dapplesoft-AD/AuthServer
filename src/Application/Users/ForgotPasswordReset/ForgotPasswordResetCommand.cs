using Application.Abstractions.Messaging;
using FluentValidation;

namespace Application.Users.ForgotPasswordReset;
public sealed record ForgotPasswordResetCommand(
    string Email,
    string NewPassword,
    string ConfirmPassword) : ICommand<ForgotPasswordResetResponse>;
public sealed class ForgotPasswordResetCommandValidator : AbstractValidator<ForgotPasswordResetCommand>
{
    public ForgotPasswordResetCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword);
    }
}
