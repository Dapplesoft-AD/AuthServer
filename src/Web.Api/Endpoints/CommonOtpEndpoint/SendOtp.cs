using Application.Abstractions.Messaging;
using Application.CommonOtp;
using Application.SmsConfigs.OtpSms;
using Application.SmtpConfigs.OtpMail;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.CommonOtpEndpoint;

internal sealed class SendOtp : IEndpoint
{
    public sealed class Request
    {
        public string Input { get; set; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("CommonOtp/SendOtp", async (
    Request request,
    ICommandHandler<SmsOtpCommand, Guid> smsHandler,
    ICommandHandler<SendOtpCommand, Guid> mailHandler,
    CancellationToken cancellationToken) =>
        {
            Result<Guid> result;

            if (CommonOtpInputValidator.IsPhone(request.Input))
            {
                result = await smsHandler.Handle(
                    new SmsOtpCommand(request.Input),
                    cancellationToken);
            }
            else if (CommonOtpInputValidator.IsEmail(request.Input))
            {
                result = await mailHandler.Handle(
                    new SendOtpCommand(request.Input),
                    cancellationToken);
            }
            else
            {
                return CustomResults.Problem(
                    "Input must be a valid email address (e.g., user@example.com) " +
                    "or phone number in international format (e.g., +1234567890).",
                    400);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.CommonOtp)
        .RequireAuthorization();
    }
}
