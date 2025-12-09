using Application.Abstractions.Messaging;
using Application.SmtpConfigs.OtpMail;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class OtpMail : IEndpoint
{
    public sealed class Request
    {
        public string RecipientEmail { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("SmtpConfig", async (
    Request request,
    ICommandHandler<SendOtpCommand, Guid> handler,
    CancellationToken cancellationToken) =>
        {
            var command = new SendOtpCommand(request.RecipientEmail);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
