using SharedKernel;

namespace Domain.EmailVerification;

public sealed record EmailVerificationCreatedDomainEvent(Guid EvId) : IDomainEvent;
