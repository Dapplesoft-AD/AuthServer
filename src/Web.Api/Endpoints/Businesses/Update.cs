using Application.Abstractions.Messaging;
using Application.Businesses.Update;
using Domain.Businesses;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Update : IEndpoint
{
    public sealed class Request
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string IndustryType { get; set; }
        public string LogoUrl { get; set; }
        public BusinessStatus Status { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/businesses/{id:guid}", static async (
        Guid id,
        Request request,
        ICommandHandler<UpdateBusinessCommand> handler,
        CancellationToken cancellationToken) =>
        {
            var command = new UpdateBusinessCommand(
                Id: request.Id,
                BusinessName: request.BusinessName,
                IndustryType: request.IndustryType,
                LogoUrl: request.LogoUrl,
                Status: request.Status
            );

            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
          .WithTags(Tags.Businesses)
          .RequireAuthorization();
    }
}
