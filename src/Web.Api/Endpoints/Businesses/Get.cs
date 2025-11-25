using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Businesses.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/businesses", async (
            IQueryHandler<GetBusinessesQuery, List<GetBusinessResponse>> handler,
            IUserContext userContext,
            CancellationToken cancellationToken) =>
        {
            var query = new GetBusinessesQuery();
            Result<List<GetBusinessResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
