using Domain.Users;

namespace Application.Abstractions.Authentication;

public interface IUserSession
{
    Task SignInAsync(User user, CancellationToken cancellationToken);
}