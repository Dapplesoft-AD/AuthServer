using Application.Abstractions.Messaging;
using Application.Applications.Get;
using Application.Applications.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Applications;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("applications", async (
            IQueryHandler<GetAllApplicationsQuery, List<ApplicationResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllApplicationsQuery();
            Result<List<ApplicationResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
