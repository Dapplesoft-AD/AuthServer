using Application.Abstractions.Messaging;
using Application.Permissions.GetAll;
using Application.Permissions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Permissions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permissions", async (
            IQueryHandler<GetAllPermissionsQuery, List<PermissionResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllPermissionsQuery();

            Result<List<PermissionResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Permissions")
        .RequireAuthorization();
    }
}
