using SharedKernel;

namespace Domain.SmsConfigs;

public static class SmsConfigErrors
{
    public static Error NotFound(Guid SmsId) => Error.NotFound(
        "SmsConfig.NotFound",
        $"The Sms Configuration with the Id = '{SmsId}' was not found");
}
