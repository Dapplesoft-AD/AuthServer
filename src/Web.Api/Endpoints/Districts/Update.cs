using Application.Abstractions.Messaging;
using Application.Districts.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Districts;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Update(Base.Districts), async (
            Guid id,
            UpdateDistrictRequest request,
            ICommandHandler<UpdateDistrictCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateDistrictCommand(
                DistrictId: id,
                CountryId: request.CountryId,
                RegionId: request.RegionId,
                Name: request.Name,
                IsActive: request.IsActive
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization()
        .WithSummary("Update District")
        .WithDescription("Updates district fields");
    }
}

public sealed record UpdateDistrictRequest(
    Guid CountryId,
    Guid RegionId,
    string Name,
    bool IsActive
);
