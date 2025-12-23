using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.GetById;

public sealed record GetSmsConfigByIdQuery(Guid SmsId) : IQuery<SmsConfigResponse>;
