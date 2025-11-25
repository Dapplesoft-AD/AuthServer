using Application.Abstractions.Messaging;
using Application.Roles.Get;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Roles;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("roles", async (
            IQueryHandler<GetRolesQuery, List<RoleResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRolesQuery();
            SharedKernel.Result<List<RoleResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Roles);
    }
}
