using Application.Abstractions.Messaging;
using Application.Regions.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Regions;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("Regions/{id:guid}", async (
            Guid id,
            UpdateRegionRequest request,
            ICommandHandler<UpdateRegionCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateRegionCommand(
                RegionId: id,
                CountryId: request.CountryId,
                Name: request.Name,
                RegionType: request.RegionType,
                IsActive: request.IsActive
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.Regions)
        .RequireAuthorization()
        .WithSummary("Update Region")
        .WithDescription("Updates region fields");
    }
}

public sealed record UpdateRegionRequest(
    Guid CountryId,
    string Name,
    string RegionType,
    bool IsActive
);
