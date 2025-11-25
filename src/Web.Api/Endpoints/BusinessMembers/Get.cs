using Application.Abstractions.Messaging;
using Application.BusinessMembers.Get;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BusinessMembers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-members", async (
            IQueryHandler<GetBusinessMembersQuery, List<BusinessMemberResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetBusinessMembersQuery();
            SharedKernel.Result<List<BusinessMemberResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.BusinessMembers)
        .RequireAuthorization();
    }
}


