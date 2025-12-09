using SharedKernel;

namespace Domain.SmtpConfigs;

public static class SmtpConfigErrors
{
    public static Error NotFound(Guid SmtpId) => Error.NotFound(
        "SmtpConfig.NotFound",
        $"The Smtp Configuration with the Id = '{SmtpId}' was not found");
}
