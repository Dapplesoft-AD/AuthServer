using SharedKernel;

namespace Domain.EmailVerification;

public sealed record EmailVerificationDeletedDomainEvent(Guid EV_Id) : IDomainEvent;
