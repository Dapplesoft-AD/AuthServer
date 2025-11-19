using Application.Abstractions.Messaging;
using Application.MfaLogs.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Domain.MfaLogs;

namespace Web.Api.Endpoints.MfaLogs;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/MfaLogs/create", async (
            Request request,
            ICommandHandler<CreateMfaLogCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateMfaLogCommand
            {
                UserId = request.UserId,
                LoginTime = request.LoginTime,
                IpAddress = request.IpAddress,
                Device = request.Device,
                Status = (MfaLogStatus)request.Status
            }; 

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization();
    }

    public sealed record Request(
        Guid UserId,
        DateTime LoginTime,
        string IpAddress,
        string Device,
        int Status
    );
}
