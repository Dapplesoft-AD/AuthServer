using Application.Abstractions.Messaging;
using Application.SubDistricts.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubDistrict;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                ApiRoutes.GetById(Base.Areas),
                async (
                    Guid id,
                    IQueryHandler<GetSubDistrictByIdQuery, SubDistrictResponse> handler,
                    CancellationToken cancellationToken
                ) =>
                {
                    var query = new GetSubDistrictByIdQuery(id);

                    Result<SubDistrictResponse> result = await handler.Handle(
                        query,
                        cancellationToken
                    );

                    return result.Match(Results.Ok, CustomResults.Problem);
                }
            )
            .WithTags(Tags.Areas)
            .RequireAuthorization();
    }
}
