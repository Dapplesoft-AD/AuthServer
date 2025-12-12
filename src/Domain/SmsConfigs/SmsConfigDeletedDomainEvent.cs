using SharedKernel;

namespace Domain.SmsConfigs;

public sealed record SmsConfigDeletedDomainEvent(Guid SmsId) : IDomainEvent;
