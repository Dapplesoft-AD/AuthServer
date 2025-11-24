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

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public UserStatus? Status { get; set; }

        public bool? IsMFAEnabled { get; set; }

        public bool? IsEmailVerified { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Users.Update, async (
            Guid id,
            Request request,
            ICommandHandler<UpdateUserCommand> handler,
            CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserCommand(
                    UserId: id,
                    Fullname: request.Fullname,
                    Email: request.Email,
                    Password: request.Password,
                    Phone: request.Phone,
                    Status: request.Status,
                    IsMFAEnabled: request.IsMFAEnabled,
                    IsEmailVerified: request.IsEmailVerified
                );

                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .HasPermission(Permissions.UsersAccess)
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
