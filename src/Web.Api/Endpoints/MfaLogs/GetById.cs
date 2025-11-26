using Application.Abstractions.Messaging;
using Application.MfaLogs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("MfaLogs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetMfaLogByIdQuery, MfaLogResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMfaLogByIdQuery(id);

            Result<MfaLogResponse> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization();
    }
}
