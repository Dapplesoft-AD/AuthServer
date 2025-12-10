using Application.Abstractions.Messaging;
using Application.SmsConfigs.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string SmsToken { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("SmsConfig/Update/{Id:Guid}", async (
            Guid Id,
            Request request,
            ICommandHandler<UpdateSmsCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateSmsCommand(
               Id,
               request.SmsToken
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Ok(id),
                error => CustomResults.Problem(error)
            );

        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
