
namespace Application.Abstractions.Otp;

public interface IOtpGenerator
{
    string GenerateOtp(int length = 4);
}
