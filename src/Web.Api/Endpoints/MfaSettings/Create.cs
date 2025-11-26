using Application.Abstractions.Messaging;
using Application.MfaSettings.Create;
using Domain.MfaSettings;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        public string BackupCodes { get; set; } = string.Empty;
        public MfaMethod Method { get; set; } // Change to enum type directly
        public bool Enabled { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("mfasettings", async (
            Request request,
            ICommandHandler<CreateMfaSettingCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateMfaSettingCommand
            {
                UserId = request.UserId,
                SecretKey = request.SecretKey,
                BackupCodes = request.BackupCodes,
                Method = request.Method, // Direct assignment
                Enabled = request.Enabled
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaSettings)
        .RequireAuthorization();
    }
}
