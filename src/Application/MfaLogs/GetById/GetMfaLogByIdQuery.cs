using System;
using Application.Abstractions.Messaging;

namespace Application.MfaLogs.GetById;

public sealed record GetMfaLogByIdQuery(Guid Id) : IQuery<MfaLogResponse>;
