using Application.Abstractions.Messaging;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FullName,
    string Password,
    string? Phone)
    : ICommand<Guid>;
