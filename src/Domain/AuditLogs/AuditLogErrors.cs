using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
