using FluentValidation;

namespace Application.ClientApps.Create;

internal class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotEmpty()
            .WithMessage("Client ID must not be empty");

        RuleFor(c => c.DisplayName)
            .NotEmpty()
            .WithMessage("DisplayName not be empty");
        
        RuleFor(c => c.ClientSecret)
            .NotEmpty()
            .WithMessage("ClientSecret must not be empty");

        RuleFor(c => c.RedirectUri)
            .NotEmpty()
            .WithMessage("RedirectUrl ID must not be empty")
            .Must(uriArray => uriArray.All(uri => BeAValidUri(uri)))
            .WithMessage("RedirectUrl must be a valid Uri");
    }

    private bool BeAValidUri(Uri redirectUri)
    {
        return redirectUri.IsAbsoluteUri;
    }
}
