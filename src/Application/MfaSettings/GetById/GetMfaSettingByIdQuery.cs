using Application.Abstractions.Messaging;

namespace Application.MfaSettings.GetById;

public sealed record GetMfaSettingByIdQuery(Guid Id)
    : IQuery<MfaSettingResponse>;
