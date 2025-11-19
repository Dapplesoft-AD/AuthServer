using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Get;

public sealed record GetPasswordResetQuery(Guid UserId) : IQuery<List<PasswordResetResponse>>;
