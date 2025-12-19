
namespace SharedKernel.Models;

public sealed class Token
{
    public string? Id { get; init; }

    public string? Subject { get; set; }

    public string? ApplicationId { get; init; }

    public string? AuthorizationId { get; init; }

    public string? ConcurrencyToken { get; init; }

    public string? Type { get; set; }

    public string? Status { get; init; }

    public DateTimeOffset? CreationDate { get; init; }

    public DateTimeOffset? ExpirationDate { get; init; }

    public string? Payload { get; init; }
}
