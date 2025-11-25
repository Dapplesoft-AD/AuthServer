using Application.Abstractions.Messaging;
using Application.EmailVerification.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime VerifiedAt { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("EmailVerifications", async (
            Request request,
            ICommandHandler<CreateEmailVerificationCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateEmailVerificationCommand
            {
                UserId = request.UserId,
                VerifiedAt = request.VerifiedAt,
                Token = request.Token,
                ExpiresAt = request.ExpiresAt,
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.EmailVerifications)
        .RequireAuthorization();
    }
}
