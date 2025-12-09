using Application.Abstractions.Messaging;
using Application.Districts.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Districts;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("Districts/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteDistrictCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteDistrictCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization()
        .WithSummary("Delete District")
        .WithDescription("Deletes a district by Id.");
    }
}
