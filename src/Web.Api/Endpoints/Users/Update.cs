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
        public Guid UserId { get; set; }
        
        public string? Fullname { get; set; }
        
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public Status? Status { get; set; }

        public bool? IsMFAEnabled {  get; set; }

        public bool? IsEmailVarified { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("user/update/{id:guid}", async(
            Guid id,
            Request request,
            ICommandHandler<UpdateUserCommand> handler,
            CancellationToken cancellationToken) => 
            {
                var command = new UpdateUserCommand(
                    
                    UserId: request.UserId,
                    Fullname: request.Fullname,
                    Email: request.Email,
                    Password: request.Password,
                    Phone: request.Phone,
                    Status: request.Status,
                    IsMFAEnabled: request.IsMFAEnabled,
                    IsEmailVarified: request.IsEmailVarified

                );

                Result result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);

            })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
