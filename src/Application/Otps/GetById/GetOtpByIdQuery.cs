using Application.Abstractions.Messaging;

namespace Application.Otps.GetById;

public sealed record GetOtpByIdQuery(Guid OtpId) : IQuery<OtpResponse>;
