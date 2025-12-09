using Application.Abstractions.Messaging;
using Application.Localities.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Localities;

internal sealed class Update : IEndpoint
{
    public sealed record Request(
        Guid CountryId,
        Guid AreaId,
        string Name,
        int Type, // 1=Union, 2=Ward, 3=Neighborhood, etc.
        bool IsActive
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("localities/{id:Guid}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdateLocalityCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            if (!Enum.IsDefined(typeof(Domain.Localities.Locality.LocalityType), request.Type))
            {
                return Results.BadRequest("Invalid locality type.");
            }

            var localityType = (Domain.Localities.Locality.LocalityType)request.Type;

            var command = new UpdateLocalityCommand(
                id,
                request.CountryId,
                request.AreaId,
                request.Name,
                localityType,
                request.IsActive
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                localityId => Results.Ok(new { Id = localityId, Message = "Locality updated successfully." }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Localities)
        .RequireAuthorization();
    }
}
