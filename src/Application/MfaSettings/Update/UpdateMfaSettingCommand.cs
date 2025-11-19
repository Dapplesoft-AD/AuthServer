using System;
using Application.Abstractions.Messaging;

namespace Application.MfaSettings.Update;

public sealed record UpdateMfaSettingCommand(
    Guid MfaSettingId,
    Guid UserId,
    string? SecretKey,
    string? BackupCodes,
    string Method,
    bool Enabled
) : ICommand;
