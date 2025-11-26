using Application.Abstractions.Messaging;
using Application.MfaLogs.Create;
using Domain.MfaLogs;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/mfalogs", async (
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
                Status = request.Status 
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
        MfaLogStatus Status 
    );
}
