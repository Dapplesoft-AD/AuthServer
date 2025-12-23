using Application.Abstractions.Messaging;
using Application.SmsConfigs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("SmsConfigs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetSmsConfigByIdQuery, SmsConfigResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new GetSmsConfigByIdQuery(id);

            Result<SmsConfigResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
