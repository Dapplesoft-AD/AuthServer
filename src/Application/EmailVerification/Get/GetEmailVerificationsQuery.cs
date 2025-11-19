using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Get;

public sealed record GetEmailVerificationsQuery(Guid UserId) : IQuery<List<EmailVerificationResponse>>;
