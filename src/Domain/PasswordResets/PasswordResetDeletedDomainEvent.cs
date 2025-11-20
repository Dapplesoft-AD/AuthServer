using SharedKernel;

namespace Domain.PasswordResets;

public sealed record PasswordResetDeletedDomainEvent(Guid PR_Id) : IDomainEvent;
