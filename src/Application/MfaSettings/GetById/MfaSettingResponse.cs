namespace Application.MfaSettings.GetById;

public sealed class MfaSettingResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string SecretKey { get; set; } = string.Empty;
    public string? BackupCodes { get; set; }
    public string Method { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
