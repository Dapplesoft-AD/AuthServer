namespace Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid OwnerUserId { get; }
}
