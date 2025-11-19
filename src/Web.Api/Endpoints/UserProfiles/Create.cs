
using Application.Abstractions.Messaging;
using Application.UserProfiles.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.UserProfiles;

// both  create and update used
internal sealed class Create : IEndpoint
{
    public sealed record Request(  // req body
        string Address,
        string City,
        string Country,
        string PostalCode,
        string ProfileImageUrl,
        DateOnly DateOfBirth);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user-profile/update/{id:guid}", async (
            Guid id,  // from route prameter
            Request request,
            ICommandHandler<CreateUserProfileCommand, Guid> handler,
            CancellationToken cancellationToken

            ) =>
        {
            var command = new CreateUserProfileCommand
            {
                UserId = id,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                ProfileImageUrl = request.ProfileImageUrl,
                DateOfBirth = request.DateOfBirth
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.UserProfile)
        .RequireAuthorization();
    }
}
