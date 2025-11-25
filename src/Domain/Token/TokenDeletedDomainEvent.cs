using SharedKernel;

namespace Domain.Token;

public sealed record TokenDeletedDomainEvent(Guid TokenId) : IDomainEvent;
