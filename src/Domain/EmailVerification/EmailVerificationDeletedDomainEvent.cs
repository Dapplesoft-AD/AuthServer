using SharedKernel;

namespace Domain.EmailVerification;

public sealed record EmailVerificationDeletedDomainEvent(Guid EvId) : IDomainEvent;
