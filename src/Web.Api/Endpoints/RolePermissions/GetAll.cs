using Application.Abstractions.Messaging;
using Application.RolePermissions.GetAll;
using Application.RolePermissions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role-permissions", async (
            IQueryHandler<GetAllRolePermissionsQuery, List<RolePermissionResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllRolePermissionsQuery();

            Result<List<RolePermissionResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
