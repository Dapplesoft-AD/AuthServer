using Application.Abstractions.Messaging;
using Application.SmtpConfigs.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("SmtpConfig/Delete/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteSmtpConfigCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteSmtpConfigCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
