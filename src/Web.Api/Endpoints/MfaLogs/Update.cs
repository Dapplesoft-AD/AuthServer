using Application.Abstractions.Data;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.MfaLogs;

public static class Update
{
    public static void MapUpdateMfaLogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/MfaLogs/{id}", async (
            Guid id,
            UpdateMfaLogRequest request,
            IApplicationDbContext context,
            CancellationToken cancellationToken) =>
        {
            MfaLog? mfaLog = await context.MfaLogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mfaLog is null)
            {
                return Results.NotFound(Result.Failure(MfaLogErrors.NotFound(id)));
            }

            if (string.IsNullOrWhiteSpace(request.IpAddress))
            {
                return Results.BadRequest("IpAddress is required.");
            }


            if (string.IsNullOrWhiteSpace(request.Device))
            {
                return Results.BadRequest("Device is required.");
            }

            mfaLog.LoginTime = request.LoginTime;
            mfaLog.IpAddress = request.IpAddress;
            mfaLog.Device = request.Device;
            mfaLog.Status = (MfaLogStatus)request.Status;
            mfaLog.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithName("UpdateMfaLog")
        .WithTags("MfaLogs")
        .RequireAuthorization()
        .WithSummary("Update an MFA Log entry")
        .WithDescription("Updates the LoginTime, IpAddress, Device and Status of an MFA Log");
    }
}

public sealed record UpdateMfaLogRequest(
    DateTime LoginTime,
    string IpAddress,
    string Device,
    int Status
);
