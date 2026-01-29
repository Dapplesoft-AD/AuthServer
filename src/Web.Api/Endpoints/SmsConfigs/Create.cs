using Application.Abstractions.Messaging;
using Application.SmsConfigs.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public string? ProviderName { get; set; }
        public string? ProviderUrl { get; set; }
        public string ApiUrl { get; set; }
        public string SmsToken { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("SmsConfig/Create", async (
            Request request,
            ICommandHandler<CreateSmsOtpCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateSmsOtpCommand
            {
                ProviderName = request.ProviderName,
                ProviderUrl = request.ProviderUrl,
                ApiUrl = request.ApiUrl,
                SmsToken = request.SmsToken
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
