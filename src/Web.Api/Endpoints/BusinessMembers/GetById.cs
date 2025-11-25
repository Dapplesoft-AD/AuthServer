using Application.Abstractions.Messaging;
using Application.BusinessMembers.GetById;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BusinessMembers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-members/{id:guid}", async (
            Guid id,
            IQueryHandler<GetBusinessMemberByIdQuery, BusinessMemberResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetBusinessMemberByIdQuery(id);
            SharedKernel.Result<BusinessMemberResponse> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.BusinessMembers)
        .RequireAuthorization();
    }
}
