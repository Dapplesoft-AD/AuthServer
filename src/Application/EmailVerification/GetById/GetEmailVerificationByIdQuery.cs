using Application.Abstractions.Messaging;

namespace Application.EmailVerification.GetById;

public sealed record GetEmailVerificationByIdQuery(Guid EvId) : IQuery<EmailVerificationResponse>;
