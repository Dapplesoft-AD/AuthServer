using System;
using SharedKernel;

namespace Domain.Regions;

public static class RegionErrors
{
    public static Error NotFound(Guid id) =>
        new Error(
            "Region.NotFound",
            $"Region with Id '{id}' was not found.",
            ErrorType.NotFound);
}
