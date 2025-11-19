using Microsoft.AspNetCore.Routing;
using Application.Abstractions.Messaging;
using Application.MfaSettings.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

public static class GetMfaSettingsEndpoint
{
    public static void MapGetMfaSettingsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/MfaSettings", async (
            IQueryHandler<GetMfaSettingQuery, List<MfaSettingResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            Result<List<MfaSettingResponse>> result =
                await handler.Handle(new GetMfaSettingQuery(), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags("MfaSettings")
        .RequireAuthorization();
    }
}
