using Application.Abstractions.Messaging;

namespace Application.Otps.GetById;

public sealed record GetOtpByIdQuery(Guid Id) : IQuery<OtpResponse>;
