using FluentValidation;

namespace Application.UserLoginHistories.Create;
public class CreateUserLoginHistoryCommandValidator : AbstractValidator<CreateUserLoginHistoryCommand>
{
    public CreateUserLoginHistoryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.IpAddress)
            .NotEmpty().WithMessage("IP Address is required.")
            .MaximumLength(50).WithMessage("IP Address cannot exceed 50 characters.")
            .Must(BeValidIp).WithMessage("IP Address must be a valid IPv4 or IPv6 address.");

        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.Browser)
            .MaximumLength(100).WithMessage("Browser cannot exceed 100 characters.");

        RuleFor(x => x.OS)
            .MaximumLength(100).WithMessage("Operating System cannot exceed 100 characters.");

        RuleFor(x => x.Device)
            .MaximumLength(50).WithMessage("Device cannot exceed 50 characters.");

        //RuleFor(x => x.LogInTime)
        //    .NotEmpty().WithMessage("Login time is required.");


        RuleFor(x => x.LogoutTime)
            .GreaterThanOrEqualTo(x => x.LogInTime)
            .When(x => x.LogoutTime.HasValue)
            .WithMessage("Logout time cannot be earlier than login time.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid login status.");
    }

    private bool BeValidIp(string ip)
    {
        return System.Net.IPAddress.TryParse(ip, out _);
    }
}
