using Application.Abstractions.Messaging;
using Application.MfaSettings.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Domain.MfaSettings;

namespace Web.Api.Endpoints.MfaSettings;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("mfasettings/{id:guid}", async (
            Guid id,
            UpdateMfaSettingRequest request,
            ICommandHandler<UpdateMfaSettingCommand> handler,
            CancellationToken cancellationToken) =>
        {
            // Validate that int matches enum values
            if (!Enum.IsDefined(typeof(MfaMethod), request.Method))
            {
                return Results.BadRequest(new
                {
                    Error = "Invalid MFA method. Allowed values: 0=TOTP, 1=SMS, 2=EMAIL"
                });
            }

            var command = new UpdateMfaSettingCommand(
                id,
                request.UserId,
                request.SecretKey,
                request.BackupCodes,
                (MfaMethod)request.Method,  // Cast int → enum
                request.Enabled
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.MfaSettings)
        .RequireAuthorization();
    }
}

public sealed record UpdateMfaSettingRequest(
    Guid UserId,
    string? SecretKey,
    string? BackupCodes,
    int Method,   // int, not string
    bool Enabled
);
