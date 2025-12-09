using Application.Abstractions.Messaging;
using Application.Users.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.User.GetAll, async (
            IQueryHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllUsersQuery();

            Result<List<GetAllUsersQueryResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .HasPermission(Permissions.UsersAccess)
        .RequireAuthorization();
    }
}
