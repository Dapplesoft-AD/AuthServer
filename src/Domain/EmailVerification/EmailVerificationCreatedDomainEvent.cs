using SharedKernel;

namespace Domain.EmailVerification;

public sealed record EmailVerificationCreatedDomainEvent(Guid EV_Id) : IDomainEvent;
