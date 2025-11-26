using System;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;


namespace Application.MfaSettings.Create;


public sealed class CreateMfaSettingCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public string SecretKey { get; set; }
    public string BackupCodes { get; set; }
    public MfaMethod Method { get; set; } 
    public bool Enabled { get; set; }
}
