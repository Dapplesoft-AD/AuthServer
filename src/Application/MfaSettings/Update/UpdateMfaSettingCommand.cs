using System;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;

namespace Application.MfaSettings.Update;

public sealed record UpdateMfaSettingCommand(
    Guid MfaSettingId,
    Guid UserId,
    string? SecretKey,
    string? BackupCodes,
    MfaMethod Method,  
    bool Enabled
) : ICommand;
