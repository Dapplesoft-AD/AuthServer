using Application.Abstractions.Messaging;
using Application.MfaSettings.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("MfaSettings/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteMfaSettingCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteMfaSettingCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.MfaSettings)
        .RequireAuthorization();
    }
}
