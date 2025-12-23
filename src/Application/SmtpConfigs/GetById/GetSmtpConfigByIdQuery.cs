using Application.Abstractions.Messaging;

namespace Application.SmtpConfigs.GetById;

public sealed record GetSmtpConfigByIdQuery(Guid SmtpId) : IQuery<SmtpConfigResponse>;
