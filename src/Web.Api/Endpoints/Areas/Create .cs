using Application.Abstractions.Messaging;
using Application.Areas.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Areas;

internal sealed class Create : IEndpoint
{
    public sealed record Request(
        Guid CountryId,
        Guid DistrictId,
        string Name,
        int Type, // 1=Upazila, 2=City, 3=Thana, 4=Municipality, 5=Township
        bool IsActive
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("areas", async (
            Request request,
            ICommandHandler<CreateAreaCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            if (!Enum.IsDefined(typeof(Domain.Areas.Area.AreaType), request.Type))
            {
                return Results.BadRequest("Invalid area type. Use 1=Upazila, 2=City, 3=Thana, 4=Municipality, 5=Township");
            }

            var areaType = (Domain.Areas.Area.AreaType)request.Type;

            var command = new CreateAreaCommand(
                request.CountryId,
                request.DistrictId,
                request.Name,
                areaType,
                request.IsActive
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Areas)
        .RequireAuthorization();
    }
}
