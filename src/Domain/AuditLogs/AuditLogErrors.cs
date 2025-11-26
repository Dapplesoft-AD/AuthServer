using System;
using SharedKernel;
namespace Domain.AuditLogs;
public static class AuditLogErrors
{
    public static Error NotFound(Guid id) =>
        new Error(
            "AuditLog.NotFound",
            $"AuditLog with Id '{id}' was not found.",
            ErrorType.NotFound);
}
