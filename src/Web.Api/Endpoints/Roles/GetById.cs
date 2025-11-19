using Application.Abstractions.Messaging;
using Application.Roles.Get;
using Application.Roles.GetById;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Roles;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("roles/{id:guid}", async (
            Guid id,
            IQueryHandler<GetRoleByIdQuery, Application.Roles.Get.RoleResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRoleByIdQuery(id);

            SharedKernel.Result<Application.Roles.Get.RoleResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                role => Results.Ok(role),
                error => error.ToProblemDetails()
            );
        })
        .WithTags("Roles");
    }
}
