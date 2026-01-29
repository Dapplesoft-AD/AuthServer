using System.Globalization;
using System.Security.Cryptography;
using Application.Abstractions.Otps;

namespace Application.Otps;

public class OtpGenerator : IOtpGenerator
{
    public string GenerateOtp(int length = 4)
    {
        // 6 digits => 000000-999999
        byte[] bytes = RandomNumberGenerator.GetBytes(4);
        uint num = BitConverter.ToUInt32(bytes, 0) % (uint)Math.Pow(10, length);
        return num.ToString($"D{length}", CultureInfo.CurrentCulture);
    }
}
