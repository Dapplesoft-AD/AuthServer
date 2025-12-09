using System;
using SharedKernel;

namespace Domain.Districts;

public static class DistrictErrors
{
    public static Error NotFound(Guid id) =>
     new Error(
         "District.NotFound",
         $"District with Id '{id}' was not found.",
         ErrorType.NotFound);
}
