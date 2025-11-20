using Application.Abstractions.Messaging;
using Application.PasswordResets.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string Token { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("PasswordResets/update/{id:guid}", async (
    Guid id,
    Request request,
    ICommandHandler<UpdatePasswordResetCommand> handler,
    CancellationToken cancellationToken) =>
        {
            var command = new UpdatePasswordResetCommand(id, request.Token);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
.WithTags(Tags.PasswordReset)
.RequireAuthorization();
    }
}
