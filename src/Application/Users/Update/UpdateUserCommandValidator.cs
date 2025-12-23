using FluentValidation;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        // Fullname — optional, but validate when present
        RuleFor(c => c.Fullname)
            .MaximumLength(100)
            .WithMessage("Fullname cannot exceed 100 characters.")
            .When(c => c.Fullname is not null);

        // Email — optional, but must be valid when provided
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(150)
            .WithMessage("Email cannot exceed 150 characters.")
            .When(c => c.Email is not null);

        // Phone — optional
        RuleFor(c => c.Phone)
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters.")
            .When(c => c.Phone is not null);
    }
}
