using Application.Abstractions.Messaging;
using Application.RolePermissions.GetByRoleId;
using Application.RolePermissions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class GetByRoleId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("roles/{roleId:guid}/permissions", async (
            Guid roleId,
            IQueryHandler<GetRolePermissionsByRoleIdQuery, List<RolePermissionResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRolePermissionsByRoleIdQuery(roleId);

            Result<List<RolePermissionResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
