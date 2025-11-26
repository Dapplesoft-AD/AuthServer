using System;
using Application.Abstractions.Messaging;

namespace Application.MfaSettings.Delete;

public sealed record DeleteMfaSettingCommand(Guid Id) : ICommand;
