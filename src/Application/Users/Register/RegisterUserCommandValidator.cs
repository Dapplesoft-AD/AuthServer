using System.Numerics;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(150);
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
    }
}
