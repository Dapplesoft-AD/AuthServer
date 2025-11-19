using Application.Abstractions.Messaging;

namespace Application.PasswordResets.GetById;

public sealed record GetPasswordResetByIdQuery(Guid Id) : IQuery<PasswordResetResponse>;
