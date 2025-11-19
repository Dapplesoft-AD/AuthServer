using Microsoft.AspNetCore.Routing;
using Application.Abstractions.Messaging;
using Application.MfaSettings.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

public static class GetMfaSettingByIdEndpoint
{
    public static void MapGetMfaSettingByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/MfaSettings/{id:guid}", async (
            Guid id,
            IQueryHandler<GetMfaSettingByIdQuery, MfaSettingResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<MfaSettingResponse> result =
                await handler.Handle(new GetMfaSettingByIdQuery(id), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags("MfaSettings")
        .RequireAuthorization();
    }
}
