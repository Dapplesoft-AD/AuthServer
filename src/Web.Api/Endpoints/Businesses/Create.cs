using Application.Abstractions.Messaging;
using Application.Businesses.Create;
using Domain.Businesses;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid OwnerUserId { get; set; }
        public string BusinessName { get; set; }
        public string IndustryType { get; set; }
        public string LogoUrl { get; set; }
        public BusinessStatus Status { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/businesses", async (
        Request request,
        ICommandHandler<CreateBusinessCommand, Guid> handler,
        CancellationToken cancellationToken) =>
        {
            var command = new CreateBusinessCommand
            {
                OwnerUserId = request.OwnerUserId,
                BusinessName = request.BusinessName,
                IndustryType = request.IndustryType,
                LogoUrl = request.LogoUrl,
                Status = request.Status,
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Created($"/businesses/{id}", new { id }),
                CustomResults.Problem
            );
        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
