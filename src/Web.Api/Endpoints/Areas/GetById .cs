using Application.Abstractions.Messaging;
using Application.Areas.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Areas;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("areas/{id:Guid}", async (
            Guid id,
            IQueryHandler<GetAreaByIdQuery, AreaResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAreaByIdQuery(id);

            Result<AreaResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Areas)
        .RequireAuthorization();
    }
}
