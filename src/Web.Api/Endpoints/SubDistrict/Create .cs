using Application.Abstractions.Messaging;
using Application.SubDistricts.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubDistrict;

internal sealed class Create : IEndpoint
{
    public sealed record Request(
        Guid CountryId,
        Guid DistrictId,
        string Name,
        int Type, 
        bool IsActive
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.Areas), async (
            Request request,
            ICommandHandler<CreateAreaCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateAreaCommand(
                request.CountryId,
                request.DistrictId,
                request.Name,
                request.IsActive
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Areas)
        .RequireAuthorization();
    }
}
