using System.Text.RegularExpressions;

namespace Application.CommonOtp;

public static partial class CommonOtpInputValidator
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^\+?[0-9]{8,15}$")]
    private static partial Regex PhoneRegex();

    public static bool IsEmail(string input) => EmailRegex().IsMatch(input);
    public static bool IsPhone(string input) => PhoneRegex().IsMatch(input);
}
