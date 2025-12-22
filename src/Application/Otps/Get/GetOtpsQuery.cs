using Application.Abstractions.Messaging;

namespace Application.Otps.Get;

public sealed record GetOtpsQuery : IQuery<List<OtpResponse>>;
