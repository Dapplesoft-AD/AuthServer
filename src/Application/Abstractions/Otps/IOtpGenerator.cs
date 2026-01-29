
namespace Application.Abstractions.Otps;

public interface IOtpGenerator
{
    string GenerateOtp(int length = 4);
}
