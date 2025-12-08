using Application.Abstractions.Messaging;
using Application.Countries.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Countries;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; } = null!;
        public string Capital { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/countries/{id:guid}", static async (
            Guid id,
            Request request,
            ICommandHandler<UpdateCountryCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCountryCommand
            {
                Id = id,
                Name = request.Name,
                Capital = request.Capital,
                PhoneCode = request.PhoneCode,
                IsActive = request.IsActive
            };

            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}
