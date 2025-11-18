using Application.Abstractions.Messaging;
using Application.UserProfiles.Create;
using Application.UserProfiles.Get;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.UserProfiles;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user-profile/{id:guid}", async (
            Guid id,
            IQueryHandler<GetUserProfileQuery, UserProfileResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetUserProfileQuery(id);

            Result<UserProfileResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.UserProfile)
        .RequireAuthorization();
    }
}
