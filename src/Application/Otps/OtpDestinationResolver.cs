

using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Application.Otps;

internal static class OtpDestinationResolver
{
    private static readonly Regex PhoneRegex =
        new(@"^(?:\+?88)?01[3-9]\d{8}$", RegexOptions.Compiled);

    public static OtpDestinationType Resolve(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Destination is empty.");
        }

        if (MailAddress.TryCreate(input, out _))
        {
            return OtpDestinationType.Email;
        }

        if (PhoneRegex.IsMatch(input))
        {
            return OtpDestinationType.Phone;
        }

        throw new ArgumentException("Invalid email or phone number.");
    }
}
