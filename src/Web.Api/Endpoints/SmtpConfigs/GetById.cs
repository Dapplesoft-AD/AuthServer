using Application.Abstractions.Messaging;
using Application.SmtpConfigs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("SmtpConfigs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetSmtpConfigByIdQuery, SmtpConfigResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new GetSmtpConfigByIdQuery(id);

            Result<SmtpConfigResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmtpConfig)
        .RequireAuthorization();
    }
}
