using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Token.Create;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(x => x.User_id)
            .NotEmpty().WithMessage("User ID is required.")
            .WithMessage("User ID must be a positive number.");
        RuleFor(x => x.App_id)
            .NotEmpty().WithMessage("App ID is required.")
            .WithMessage("App ID must be a positive number.");
        RuleFor(x => x.Acess_token)
            .NotEmpty().WithMessage("Access token is required.")
            .MaximumLength(500).WithMessage("Access token cannot exceed 500 characters.");
        RuleFor(x => x.Refresh_token)
            .NotEmpty().WithMessage("Refresh token is required.")
            .MaximumLength(500).WithMessage("Refresh token cannot exceed 500 characters.");
        RuleFor(x => x.Issued_at)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Issued at cannot be in the future.");
    }
}
