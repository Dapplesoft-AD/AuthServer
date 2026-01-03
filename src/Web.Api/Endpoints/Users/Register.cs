using Application.Abstractions.Messaging;
using Application.Users.Register;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(
        string Email, 
        string FullName, 
        string Password, 
        string? Phone, 
        Guid? CountryId,
        Guid? RegionId,
        Guid? DistrictId,
        Guid? SubDistrictId
    );   // gets from client

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Register(Base.User), async (
            Request request,
            ICommandHandler<RegisterUserCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FullName,
                request.Password,
                request.Phone,
                request.CountryId,
                request.RegionId,
                request.DistrictId,
                request.SubDistrictId
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
