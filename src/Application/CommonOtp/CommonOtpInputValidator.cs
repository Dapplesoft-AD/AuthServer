using System.Text.RegularExpressions;

namespace Application.CommonOtp;

public static partial class CommonOtpInputValidator
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^\+\d{1,3}\d{4,14}(?:x.+)?$")]
    private static partial Regex PhoneRegex();

    public static bool IsEmail(string input) => EmailRegex().IsMatch(input);
    public static bool IsPhone(string input) => PhoneRegex().IsMatch(input);
}
