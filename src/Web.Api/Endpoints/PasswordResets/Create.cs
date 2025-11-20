using Application.Abstractions.Messaging;
using Application.PasswordResets.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("PasswordResets", async (
            Request request,
            ICommandHandler<CreatePasswordResetCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreatePasswordResetCommand
            {
                ExpiresAt = request.ExpiresAt,
                Token = request.Token,
                Used = request.Used,
                UserId = request.UserId
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PasswordReset)
        .RequireAuthorization();
    }
}
