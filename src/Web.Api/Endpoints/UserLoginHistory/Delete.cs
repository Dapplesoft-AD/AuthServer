using Application.Abstractions.Messaging;
using Application.UserLoginHistories.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.UserLoginHistory;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.UserLoginHistory.Create, async (
            Guid id,
            ICommandHandler<DeleteUserloginHistoryCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteUserloginHistoryCommand(id);

            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.UserLoginHistory)
        .RequireAuthorization();
    }
}
