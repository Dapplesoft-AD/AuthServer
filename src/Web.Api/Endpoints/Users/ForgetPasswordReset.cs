using Application.Abstractions.Messaging;
using Application.Users.ForgetPasswordReset;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;
internal sealed class ForgetPasswordReset : IEndpoint
{
    public sealed record Request(
        string Email,
        string NewPassword,
        string ConfirmPassword);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/forget-password-reset", async (
            Request request,
            ICommandHandler<ForgetPasswordResetCommand, ForgetPasswordResetResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ForgetPasswordResetCommand(
                request.Email,
                request.NewPassword,
                request.ConfirmPassword);

            Result<ForgetPasswordResetResponse> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .AllowAnonymous();
    }
}
