using System;
using Application.Abstractions.Messaging;

namespace Application.MfaLogs.Delete;

public sealed record DeleteMfaLogCommand(Guid Id) : ICommand;
