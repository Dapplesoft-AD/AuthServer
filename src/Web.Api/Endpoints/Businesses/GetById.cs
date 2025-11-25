using Application.Abstractions.Messaging;
using Application.Businesses.GetById;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/businesses/{id:guid}", async (
        Guid id,
        IQueryHandler<GetBusinessByIdQuery, GetBusinessByIdResponse> handler,
        CancellationToken cancellationToken) =>
        {
            var query = new GetBusinessByIdQuery(id);
            SharedKernel.Result<GetBusinessByIdResponse> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
