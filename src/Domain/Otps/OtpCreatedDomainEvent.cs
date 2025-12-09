using SharedKernel;

namespace Domain.Otps;

public sealed record OtpCreatedDomainEvent(Guid OtpId) : IDomainEvent;
