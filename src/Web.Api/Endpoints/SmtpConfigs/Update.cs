using Application.Abstractions.Messaging;
using Application.SmtpConfigs.SmtpUpdate;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class UpdateSmtp : IEndpoint
{
    public sealed class Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("SmtpConfigs/Update/{Id:Guid}", async (
            Guid Id,
            Request request,
            ICommandHandler<UpdateSmtpCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateSmtpCommand(
                Id,
                request.Username,
                request.Password,
                request.SenderEmail
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Ok(id),
                error => CustomResults.Problem(error)
            );

        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
