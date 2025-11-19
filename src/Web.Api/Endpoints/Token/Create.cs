using Application.Abstractions.Messaging;
using Application.Token.Create;
using Domain.Token;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid User_id { get; set; }
        public Guid App_id { get; set; }
        public string Acess_token { get; set; } //text
        public string Refresh_token { get; set; } // text
        public DateTime Issued_at { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Token", async (
            Request request,
            ICommandHandler<CreateTokenCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTokenCommand
            {
                User_id = request.User_id,
                App_id = request.App_id,
                Acess_token = request.Acess_token,
                Refresh_token = request.Refresh_token,
                Issued_at = request.Issued_at
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
