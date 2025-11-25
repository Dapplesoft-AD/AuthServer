using Application.Abstractions.Messaging;
using Application.PasswordResets.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("PasswordResets", async (
            Guid userId,
            IQueryHandler<GetPasswordResetQuery, List<PasswordResetResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPasswordResetQuery(userId);

            Result<List<PasswordResetResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PasswordReset)
        .RequireAuthorization();
    }
}
