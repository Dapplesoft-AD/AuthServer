using Application.Abstractions.Messaging;
using Application.PasswordResets.Create;
using Domain.PasswordResets;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid User_Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires_at { get; set; }
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
                Expires_at = request.Expires_at,
                Token = request.Token,
                Used = request.Used,
                User_Id = request.User_Id
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PasswordReset)
        .RequireAuthorization();
    }
}
