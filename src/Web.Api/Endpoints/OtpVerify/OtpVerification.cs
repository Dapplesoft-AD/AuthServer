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
        public string Input { get; set; } = string.Empty;
        public string OtpToken { get; set; } = string.Empty; 
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.OtpVerify), async (
    Request request,
    ICommandHandler<VerifyOtpCommand, bool> handler,
    CancellationToken cancellationToken) =>
        {
            VerifyOtpCommand command = CommonOtpInputValidator.IsPhone(request.Input)
            ? new VerifyOtpCommand(null, request.Input, request.OtpToken)
            : new VerifyOtpCommand(request.Input, null, request.OtpToken);

            Result<bool> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.VerifyOtp)
        .RequireAuthorization();
    }
}
