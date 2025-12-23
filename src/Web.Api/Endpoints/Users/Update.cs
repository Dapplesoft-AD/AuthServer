using Application.Abstractions.Messaging;
using Application.Users.Update;
using Domain.Users;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public UserStatus? Status { get; set; }

        public bool? IsMFAEnabled { get; set; }

        public bool? IsEmailVerified { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Update(Base.Users), async (
            Guid id,
            Request request,
            ICommandHandler<UpdateUserCommand, UpdateUserResponse> handler,
            CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserCommand(
                    UserId: id,
                    Fullname: request.Fullname,
                    Email: request.Email,
                    Phone: request.Phone,
                    Status: request.Status,
                    IsMFAEnabled: request.IsMFAEnabled,
                    IsEmailVerified: request.IsEmailVerified
                );

                Result<UpdateUserResponse> result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.UsersAccess)
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
