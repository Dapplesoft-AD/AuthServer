using Application.Abstractions.Messaging;
using Application.Districts.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Districts;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid RegionId { get; set; }
        public string Name { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.Districts), async (
            Request request,
            ICommandHandler<CreateDistrictCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateDistrictCommand
            {
                RegionId = request.RegionId,
                Name = request.Name
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization()
        .WithSummary("Create a new District")
        .WithDescription("Creates a new district with given name, region, and active status.");
    }
}
