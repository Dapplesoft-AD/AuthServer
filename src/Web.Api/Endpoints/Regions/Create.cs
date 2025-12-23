using System;
using Application.Abstractions.Messaging;
using Application.Regions.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Regions;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid CountryId { get; set; }  
        public string Name { get; set; }
        public string RegionType { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.Regions), async (
            Request request,
            ICommandHandler<CreateRegionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRegionCommand
            {
                CountryId = request.CountryId,
                Name = request.Name,
                RegionType = request.RegionType,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Regions)
        .WithSummary("Creates a new region")
        .WithDescription("This endpoint creates a new region with the provided details. Requires authentication.")
        .RequireAuthorization();
    }
}
