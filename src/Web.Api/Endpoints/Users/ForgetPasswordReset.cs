using Application.Abstractions.Messaging;
using Application.Users.ForgotPasswordReset;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;
internal sealed class ForgotPasswordReset : IEndpoint
{
    public sealed record Request(
        string Email,
        string NewPassword,
        string ConfirmPassword);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/forgot-password-reset", async (
            Request request,
            ICommandHandler<ForgotPasswordResetCommand, ForgotPasswordResetResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ForgotPasswordResetCommand(
                request.Email,
                request.NewPassword,
                request.ConfirmPassword);

            Result<ForgotPasswordResetResponse> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
        
    }
}
