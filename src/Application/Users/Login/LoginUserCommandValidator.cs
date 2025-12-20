using System;
using System.Collections.Generic;
using System.Text;
using Application.Users.Update;
using FluentValidation;

namespace Application.Users.Login;

internal class LoginUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public LoginUserCommandValidator()
    {

        RuleFor(c => c.Fullname)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Fullname cannot exceed 100 characters.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(150)
            .WithMessage("Email cannot exceed 150 characters.");

        RuleFor(c => c.Phone)
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters.");
    }
}
