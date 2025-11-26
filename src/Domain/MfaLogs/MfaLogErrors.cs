
using SharedKernel;

namespace Domain.MfaLogs;

public static class MfaLogErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("MfaLog.NotFound", $"MfaLog with Id {id} not found.");
}

