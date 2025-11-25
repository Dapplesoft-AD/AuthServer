using Application.Abstractions.Messaging;
using Application.Token.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
        public string Accesstoken { get; set; } //text
        public string Refreshtoken { get; set; } // text
        public DateTime IssuedAt { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Tokens", async (
            Request request,
            ICommandHandler<CreateTokenCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTokenCommand
            {
                UserId = request.UserId,
                AppId = request.AppId,
                Accesstoken = request.Accesstoken,
                Refreshtoken = request.Refreshtoken,
                IssuedAt = request.IssuedAt
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
