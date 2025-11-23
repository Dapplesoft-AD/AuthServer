using Application.Abstractions.Messaging;
using Application.Applications.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Applications;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("applications/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteApplicationCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteApplicationCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Application deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
