using Application.Abstractions.Messaging;
using Application.SmsConfigs.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("SmsConfig/Delete/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteSmsConfigCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteSmsConfigCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
