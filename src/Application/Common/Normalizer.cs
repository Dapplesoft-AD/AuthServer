
namespace Application.Common;

public static class Normalizer
{
    public static string PhoneNumber(string? phone = "")
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return phone;
        }

        phone = phone
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("+", "");

        // Bangladesh example
        if (phone.StartsWith('0'))
        {
            phone = "88" + phone;
        }

        return phone;
    }

    public static string EmailAddressLowerCase(string? email = "")
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return email ?? string.Empty;
        }

        return ToAsciiLower(email.Trim());
    }

    private static string ToAsciiLower(string value)
    {
        char[] chars = value.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];

            // ASCII A–Z → a–z
            if (c >= 'A' && c <= 'Z')
            {
                chars[i] = (char)(c + 32);
            }
        }

        return new string(chars);
    }
}
