using SharedKernel;

namespace Domain.SmtpConfigs;

public sealed record SmtpConfigCreatedDomainEvent(Guid SmtpId) : IDomainEvent;
