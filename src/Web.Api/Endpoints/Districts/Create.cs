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
        public Guid CountryId { get; set; }
        public Guid RegionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Districts", async (
            Request request,
            ICommandHandler<CreateDistrictCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateDistrictCommand
            {
                CountryId = request.CountryId,
                RegionId = request.RegionId,
                Name = request.Name,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization();
    }
}
