using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.Get;

public sealed record GetSmsConfigsQuery : IQuery<List<SmsConfigResponse>>;
