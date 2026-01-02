using Application.Abstractions.Messaging;
using Application.SubDistricts.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubDistrict;

internal sealed class Update : IEndpoint
{
    public sealed record Request(
        Guid CountryId,
        Guid DistrictId,
        string Name,
        bool IsNew
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                ApiRoutes.Update(Base.Areas),
                async (
                    Guid id,
                    Request request,
                    ICommandHandler<UpdateSubDistrictCommand, Guid> handler,
                    CancellationToken cancellationToken
                ) =>
                {
                    var command = new UpdateSubDistrictCommand(
                        id,
                        request.DistrictId,
                        request.Name,
                        request.IsNew
                    );

                    Result<Guid> result = await handler.Handle(command, cancellationToken);

                    return result.Match(
                        areaId =>
                            Results.Ok(new { Id = areaId, Message = "Area updated successfully." }),
                        CustomResults.Problem
                    );
                }
            )
            .WithTags(Tags.Areas)
            .RequireAuthorization();
    }
}
