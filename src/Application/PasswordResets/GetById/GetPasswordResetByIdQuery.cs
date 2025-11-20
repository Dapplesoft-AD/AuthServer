using Application.Abstractions.Messaging;

namespace Application.PasswordResets.GetById;

public sealed record GetPasswordResetByIdQuery(Guid PR_Id) : IQuery<PasswordResetResponse>;
