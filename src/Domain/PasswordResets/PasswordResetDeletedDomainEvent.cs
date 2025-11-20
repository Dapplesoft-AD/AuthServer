using SharedKernel;

namespace Domain.PasswordResets;

public sealed record PasswordResetDeletedDomainEvent(Guid PrId) : IDomainEvent;
