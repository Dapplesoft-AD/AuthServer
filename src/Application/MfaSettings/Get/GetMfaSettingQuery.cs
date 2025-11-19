using System.Collections.Generic;
using Application.Abstractions.Messaging;

namespace Application.MfaSettings.Get;

public sealed record GetMfaSettingQuery()
    : IQuery<List<MfaSettingResponse>>;
