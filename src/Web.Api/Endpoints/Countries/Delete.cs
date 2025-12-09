using Application.Abstractions.Messaging;
using Application.Countries.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Countries;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/countries/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteCountryCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCountryCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}
