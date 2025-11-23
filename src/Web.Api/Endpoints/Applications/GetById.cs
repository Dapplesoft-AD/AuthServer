using Application.Abstractions.Messaging;
using Application.Applications.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Applications;
internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("applications/{id:guid}", async (
            Guid id,
            IQueryHandler<GetApplicationByIdQuery, ApplicationResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetApplicationByIdQuery(id);

            Result<ApplicationResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
