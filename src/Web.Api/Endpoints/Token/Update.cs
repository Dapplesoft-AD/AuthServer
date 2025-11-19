using Application.Abstractions.Messaging;
using Application.Token.Update;
using Domain.Enums;
using Domain.Users;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public Guid Id { get; set; }
        public Guid App_id { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("Tokens/update/{id:guid}", async (
    Guid id,
    Request request,
    ICommandHandler<UpdateTokenCommand> handler,
    CancellationToken cancellationToken) =>
        {
            var command = new UpdateTokenCommand(request.Id, request.App_id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
.WithTags(Tags.Token)
.RequireAuthorization();
    }
}
