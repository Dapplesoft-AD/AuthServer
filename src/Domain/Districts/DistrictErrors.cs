using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace Domain.Districts;

public static class DistrictErrors
{
    public static Error NotFound(Guid id) =>
       new Error(
           "Region.NotFound",
           $"Region with Id '{id}' was not found.",
           ErrorType.NotFound);
}
