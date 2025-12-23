using Application.Abstractions.Messaging;
using Application.Localities.Create;
using Domain.Localities;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Localities;

internal sealed class Create : IEndpoint
{
    public sealed record Request(
        Guid CountryId,
        Guid AreaId,
        string Name,
        int Type, 
        bool IsActive
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.Localities), async (
            Request request,
            ICommandHandler<CreateLocalityCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            // Correct enum access: Locality.LocalityType (nested enum)
            if (!Enum.IsDefined(typeof(Locality.LocalityType), request.Type))
            {
                return Results.BadRequest("Invalid locality type.");
            }

            var localityType = (Locality.LocalityType)request.Type;

            var command = new CreateLocalityCommand(
                request.CountryId,
                request.AreaId,
                request.Name,
                localityType,
                request.IsActive
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Localities)
        .RequireAuthorization();
    }
}
