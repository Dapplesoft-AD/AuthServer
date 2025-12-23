using SharedKernel;

namespace Domain.SmtpConfigs;

public static class SmtpConfigErrors
{
    public static Error NotFound(Guid SmtpId) => Error.NotFound(
        "SmtpConfig.NotFound",
        $"The Smtp Configuration with the Id = '{SmtpId}' was not found");
    public static Error AlreadyExists(string senderEmail) => Error.Conflict(
        "SmtpConfig.AlreadyExists",
        $"The Smtp Configuration with the Sender Email = '{senderEmail}' already exists");
}
