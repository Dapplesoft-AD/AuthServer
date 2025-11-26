using Application.Abstractions.Messaging;

namespace Application.MfaLogs.Get;

public sealed record GetMfaLogQuery : IQuery<List<MfaLogResponse>>;
