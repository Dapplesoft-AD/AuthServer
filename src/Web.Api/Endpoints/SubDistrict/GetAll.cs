using Application.Abstractions.Messaging;
using Application.SubDistricts.Get;
using Application.SubDistricts.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubDistrict;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                ApiRoutes.GetAll(Base.Areas),
                async (
                    IQueryHandler<GetAllSubDistrictQuery, List<SubDistrictResponse>> handler,
                    CancellationToken cancellationToken
                ) =>
                {
                    var query = new GetAllSubDistrictQuery();
                    Result<List<SubDistrictResponse>> result = await handler.Handle(
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
