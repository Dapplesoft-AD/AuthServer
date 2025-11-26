using System;
using System.Text.Json.Serialization;
using Domain.Users;
using SharedKernel;

namespace Domain.MfaSettings;

public class MfaSetting : Entity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string SecretKey { get; set; } = string.Empty;

    public string? BackupCodes { get; set; }

    public MfaMethod Method { get; set; }

    public bool Enabled { get; set; }
}


public enum MfaMethod
{
    TOTP = 0,
    SMS = 1,
    EMAIL = 2
}
