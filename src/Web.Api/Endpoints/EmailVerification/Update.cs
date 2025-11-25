using Application.Abstractions.Messaging;
using Application.EmailVerification.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmailVerification;

public sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string Token { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("EmailVerifications/update/{id:guid}", async (
    Guid id,
    Request request,
    ICommandHandler<UpdateEmailVerificationCommand> handler,
    CancellationToken cancellationToken) =>
        {
            var command = new UpdateEmailVerificationCommand(id, request.Token);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
.WithTags(Tags.EmailVerifications)
.RequireAuthorization();
    }
}
