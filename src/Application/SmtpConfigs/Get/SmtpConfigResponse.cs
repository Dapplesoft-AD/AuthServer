namespace Application.SmtpConfigs.Get;

public sealed class SmtpConfigResponse
{
    public Guid SmtpId { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
    public string SenderEmail { get; set; }
}
