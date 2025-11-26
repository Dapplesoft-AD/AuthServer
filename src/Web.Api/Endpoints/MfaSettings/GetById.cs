using Application.Abstractions.Messaging;
using Application.MfaSettings.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("MfaSettings/{id:guid}", async (
            Guid id,
            IQueryHandler<GetMfaSettingByIdQuery, MfaSettingResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMfaSettingByIdQuery(id);

            Result<MfaSettingResponse> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaSettings)
        .RequireAuthorization();
    }
}
