using SharedKernel;

namespace Domain.SmtpConfigs;

public sealed record SmtpConfigUpdatedDomainEvent(Guid SmtpId) : IDomainEvent;
