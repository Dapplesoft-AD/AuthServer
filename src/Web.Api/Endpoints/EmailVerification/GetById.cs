using Application.Abstractions.Messaging;
using Application.EmailVerification.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("EmailVerification/{id:guid}", async (
            Guid id,
            IQueryHandler<GetEmailVerificationByIdQuery, EmailVerificationResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetEmailVerificationByIdQuery(id);

            Result<EmailVerificationResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.EmailVerifications)
        .RequireAuthorization();
    }
}
