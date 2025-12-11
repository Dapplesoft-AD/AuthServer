using Application.Abstractions.Messaging;
using Application.CommonOtp;
using Application.Otps.Verify;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.OtpVerify;

internal sealed class OtpVerification : IEndpoint
{
    public sealed class Request
    {
        public string Input { get; set; }
        public string OtpToken { get; set; }    
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Otp/Verify", async (
    Request request,
    ICommandHandler<VerifyOtpCommand, bool> handler,
    CancellationToken cancellationToken) =>
        {
            if (CommonOtpInputValidator.IsPhone(request.Input))
            {
                var command = new VerifyOtpCommand(null,request.Input,request.OtpToken);

                Result<bool> result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            }
            else if (CommonOtpInputValidator.IsEmail(request.Input))
            {
                var command = new VerifyOtpCommand(request.Input,null,request.OtpToken);
                Result<bool> result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            }
            else
            {
                return CustomResults.Problem("Input must be a valid email address (e.g., user@example.com) or " +
                    "phone number in international format (e.g., +1234567890).", 400);
            }
        })
        .WithTags(Tags.VerifyOtp)
        .RequireAuthorization();
    }
}
