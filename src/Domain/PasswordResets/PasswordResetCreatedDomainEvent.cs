using SharedKernel;

namespace Domain.PasswordResets;

public sealed record PasswordResetCreatedDomainEvent(Guid PR_Id) : IDomainEvent;
