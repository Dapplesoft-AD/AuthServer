using Application.Abstractions.Messaging;

namespace Application.SmtpConfigs.Get;

public sealed record GetSmtpConfigQuery() : IQuery<List<SmtpConfigResponse>>;
