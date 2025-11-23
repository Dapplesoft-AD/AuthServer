using Application.Abstractions.Messaging;
using Application.BusinessMembers.Update;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BusinessMembers;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/business-members/{id:guid}",async(
        Guid id,
        UpdateBusinessMemberCommand request,
        ICommandHandler<UpdateBusinessMemberCommand, Guid> handler,
        CancellationToken cancellationToken) =>
        {
            UpdateBusinessMemberCommand command = request with { Id = id };
            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
       .WithTags(Tags.BusinessMembers)
       .RequireAuthorization();
    }
}
