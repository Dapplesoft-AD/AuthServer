using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role-permissions/{roleId:guid}/permissions/{permissionId:guid}", async (
            Guid roleId,
            Guid permissionId,
            IQueryHandler<GetRolePermissionQuery, RolePermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRolePermissionQuery(roleId, permissionId);

            Result<RolePermissionResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
