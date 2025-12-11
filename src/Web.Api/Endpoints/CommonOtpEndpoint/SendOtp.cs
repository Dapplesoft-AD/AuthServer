using System.Text.RegularExpressions;
using Application.Abstractions.Messaging;
using Application.SmsConfigs.OtpSms;
using Application.SmtpConfigs.OtpMail;
using Application.CommonOtp;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.CommonOtpEndpoint;

internal sealed class SendOtp : IEndpoint
{
    public sealed class Request
    {
        public string Input { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("CommonOtp/SendOtp", async (
    Request request,
    ICommandHandler<SmsOtpCommand, Guid> handler,
    ICommandHandler<SendOtpCommand, Guid> mailHandler,
    CancellationToken cancellationToken) =>
        {
            if (CommonOtpInputValidator.IsPhone(request.Input))
            {
                var command = new SmsOtpCommand(request.Input);

                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            }
            else if (CommonOtpInputValidator.IsEmail(request.Input))
            {
                var command = new SendOtpCommand(request.Input);
                Result<Guid> result = await mailHandler.Handle(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            }
            else
            {
                return CustomResults.Problem("Input must be a valid email address or phone number.", 400);
            }
        })
        .WithTags(Tags.CommonOtp)
        .RequireAuthorization();
    }
}
