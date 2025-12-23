using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Users.GetAll;
using Polly;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Users), async (
            IUserContext userContext,
            IQueryHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            if (!userContext.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            var query = new GetAllUsersQuery();

            Result<List<GetAllUsersQueryResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
