using Application.Abstractions.Messaging;
using Application.Roles.GetById;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

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
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Roles);
    }
}
