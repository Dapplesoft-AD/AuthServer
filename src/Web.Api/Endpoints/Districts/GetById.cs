using Application.Abstractions.Messaging;
using Application.Districts.Get;
using Application.Districts.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Districts;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("Districts/{id:guid}", async (
            Guid id,
            IQueryHandler<GetDistrictByIdQuery, DistrictResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetDistrictByIdQuery(id);

            Result<DistrictResponse> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization()
        .WithSummary("Get District by Id")
        .WithDescription("Returns a district by Id");
    }
}
