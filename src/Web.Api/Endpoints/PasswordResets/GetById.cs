using Application.Abstractions.Messaging;
using Application.PasswordResets.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("PasswordResets/{id:guid}", async (
            Guid id,
            IQueryHandler<GetPasswordResetByIdQuery, PasswordResetResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPasswordResetByIdQuery(id);

            Result<PasswordResetResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PasswordReset)
        .RequireAuthorization();
    }
}
