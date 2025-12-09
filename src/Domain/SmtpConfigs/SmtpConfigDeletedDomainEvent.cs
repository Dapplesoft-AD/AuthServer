using SharedKernel;

namespace Domain.SmtpConfigs;

public sealed record SmtpConfigDeletedDomainEvent(Guid SmtpId) : IDomainEvent;
