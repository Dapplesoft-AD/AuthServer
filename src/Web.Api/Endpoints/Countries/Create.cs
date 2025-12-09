using Application.Abstractions.Messaging;
using Application.Countries.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Countries;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string PhoneCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/countries", async (
            Request request,
            ICommandHandler<CreateCountryCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCountryCommand
            {
                Name = request.Name,
                Capital = request.Capital,
                PhoneCode = request.PhoneCode,
                IsActive = request.IsActive,
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Created($"/countries/{id}", new { id }),
                CustomResults.Problem
            );
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}
