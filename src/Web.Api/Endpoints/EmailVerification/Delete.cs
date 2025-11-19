using Application.Abstractions.Messaging;
using Application.EmailVerification.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("EmailVerifications/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteEmailVerificationCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteEmailVerificationCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.EmailVerifications)
        .RequireAuthorization();
    }
}
