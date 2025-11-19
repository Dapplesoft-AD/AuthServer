using SharedKernel;

namespace Domain.PasswordResets;

public sealed record PasswordResetCreatedDomainEvent(Guid Id) : IDomainEvent;
