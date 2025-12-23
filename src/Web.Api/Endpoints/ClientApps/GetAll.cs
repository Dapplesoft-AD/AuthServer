using Application.Abstractions.Messaging;
using Application.Abstractions.Openiddict;
using Application.ClientApps.Delete;
using Application.ClientApps.GetAll;
using SharedKernel;
using SharedKernel.Models;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ClientApps;

public sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.ClientApps), async (
            IQueryHandler<GetAllClientQuery, IReadOnlyList<ClientView>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllClientQuery();
            Result<IReadOnlyList<ClientView>> result = await handler.Handle(query, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ClientApps)
        .RequireAuthorization();
    }
}
