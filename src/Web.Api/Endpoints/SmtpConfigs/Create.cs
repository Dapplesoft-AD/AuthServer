using Application.Abstractions.Messaging;
using Application.SmtpConfigs.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("SmtpConfig/Create", async (
            Request request,
            ICommandHandler<CreateSmtpConfigCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateSmtpConfigCommand
            {
                Host = request.Host,
                Port = request.Port,
                Username = request.Username,
                Password = request.Password,
                SenderEmail = request.SenderEmail
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
