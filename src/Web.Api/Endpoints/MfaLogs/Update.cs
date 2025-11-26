using Application.Abstractions.Messaging;
using Application.MfaLogs.Update;
using Domain.MfaLogs;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("MfaLogs/{id:guid}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdateMfaLogCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateMfaLogCommand(
                MfaLogId: id,
                LoginTime: request.LoginTime,
                IpAddress: request.IpAddress,
                Device: request.Device,
                Status: request.Status
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Id = id, Message = "MFA log updated successfully." }),
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization()
        .WithSummary("Update an MFA Log entry")
        .WithDescription("Updates the LoginTime, IpAddress, Device and Status of an MFA Log");
    }

    public sealed record Request(
        DateTime LoginTime,
        string IpAddress,
        string Device,
        MfaLogStatus Status
    );
}
