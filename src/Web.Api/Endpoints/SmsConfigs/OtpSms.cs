using Application.Abstractions.Messaging;
using Application.SmsConfigs.OtpSms;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

internal sealed class OtpSms : IEndpoint
{
    public sealed class Request
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("SmsConfig/SendSms", async (
    Request request,
    ICommandHandler<SmsOtpCommand, Guid> handler,
    CancellationToken cancellationToken) =>
        {
            var command = new SmsOtpCommand(request.PhoneNumber);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
