using SharedKernel;

namespace Domain.SmsConfigs;

public sealed record SmsConfigCreatedDomainEvent(Guid SmsId) : IDomainEvent;
