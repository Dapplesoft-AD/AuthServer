using Application.Abstractions.Messaging;
using Application.EmailVerification.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("EmailVerifications", async (
            Guid userId,
            IQueryHandler<GetEmailVerificationsQuery, List<EmailVerificationResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetEmailVerificationsQuery(userId);

            Result<List<EmailVerificationResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.EmailVerifications)
        .RequireAuthorization();
    }
}
