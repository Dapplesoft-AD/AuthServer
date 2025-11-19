using SharedKernel;

namespace Domain.EmailVerification;

public sealed record EmailVerificationCompletedDomainEvent(Guid Id) : IDomainEvent;
