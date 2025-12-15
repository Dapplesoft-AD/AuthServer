using Application.Abstractions.Messaging;
using Application.SmtpConfigs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("SmtpConfig", async (
            IQueryHandler<GetSmtpConfigQuery, List<SmtpConfigResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetSmtpConfigQuery();

            Result<List<SmtpConfigResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
