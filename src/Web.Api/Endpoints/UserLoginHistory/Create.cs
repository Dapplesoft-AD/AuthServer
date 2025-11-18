using Application.Abstractions.Messaging;
using Application.UserLoginHistories.Create;
using Domain.UserLoginHistories;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.UserLoginHistory;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
        //public DateTime? LogInTime { get; set; }
        //public DateTime? LogoutTime { get; set; }
        public int Status { get; set; } = 1; // Default value = login succeed
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("UserLoginHistory", async (
            Request request,
            ICommandHandler<CreateUserLoginHistoryCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateUserLoginHistoryCommand
            {
                UserId = request.UserId,
                IpAddress = request.IpAddress,
                Country = request.Country,
                City = request.City,
                Browser = request.Browser,
                OS = request.OS,
                Device = request.Device,
                //LogInTime = request.LogInTime,
                //LogoutTime = request.LogoutTime,
                Status = (Status)request.Status
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.UserLoginHistory)
        .RequireAuthorization();
    }
}
