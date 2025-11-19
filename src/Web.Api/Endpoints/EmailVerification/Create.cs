using Application.Abstractions.Messaging;
using Application.EmailVerification.Create;
using Domain.EmailVerification;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid User_Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires_at { get; set; }
        public DateTime Verified_at { get; set; }
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
                User_Id = request.User_Id,
                Verified_at = request.Verified_at,
                Token = request.Token,
                Expires_at = request.Expires_at,
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.EmailVerifications)
        .RequireAuthorization();
    }
}
