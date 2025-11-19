using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.Abstractions.Messaging;
using Application.Application.Create;

namespace Application.Application.Create;

public class CreateApplicationValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Client_id)
            .NotEmpty().WithMessage("Client ID is required.")
            .MaximumLength(50).WithMessage("Client ID must not exceed 50 characters.");
        RuleFor(x => x.Client_secret)
            .NotEmpty().WithMessage("Client Secret is required.")
            .MaximumLength(100).WithMessage("Client Secret must not exceed 100 characters.");
        RuleFor(x => x.Redirect_url)
            .NotEmpty().WithMessage("Redirect URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Redirect URL must be a valid URL.");
        RuleFor(x => x.Api_base_url)
            .NotEmpty().WithMessage("API Base URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("API Base URL must be a valid URL.");
        RuleFor(x => x.Application_status)
            .IsInEnum().WithMessage("Application status is invalid.");
    }
}
