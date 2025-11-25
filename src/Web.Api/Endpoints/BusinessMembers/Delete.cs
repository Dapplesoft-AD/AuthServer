using Application.Abstractions.Messaging;
using Application.BusinessMembers.Delete;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BusinessMembers;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/business-members/{id:guid}", async (
        Guid id,
        ICommandHandler<DeleteBusinessMemberCommand, Guid> handler,
        CancellationToken cancellationToken) =>
        {
            var command = new DeleteBusinessMemberCommand(id);
            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.BusinessMembers);
    }
}
