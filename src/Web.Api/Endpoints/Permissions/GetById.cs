using Application.Abstractions.Messaging;
using Application.Permissions.GetById;
using Application.Permissions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Permissions;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permissions/{id:guid}", async (
            Guid id,
            IQueryHandler<GetPermissionByIdQuery, PermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPermissionByIdQuery(id);

            Result<PermissionResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Permissions")
        .RequireAuthorization();
    }
}
