using Application.Abstractions.Messaging;

namespace Application.PasswordResets.GetById;

public sealed record GetPasswordResetByIdQuery(Guid PrId) : IQuery<PasswordResetResponse>;
