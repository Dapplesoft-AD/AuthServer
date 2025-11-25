using Application.Abstractions.Messaging;
using Application.Businesses.Update;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/businesses/{id:guid}", static async (
        Guid id,
        UpdateBusinessCommand request,
        ICommandHandler<UpdateBusinessCommand, Guid> handler,
        CancellationToken cancellationToken) =>
        {
            if (id != request.Id)
            {
                return CustomResults.Problem("Route Id and body Id must match.");
            }

            SharedKernel.Result<Guid> result = await handler.Handle(request, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
          .WithTags(Tags.Businesses)
          .RequireAuthorization();
    }
}
