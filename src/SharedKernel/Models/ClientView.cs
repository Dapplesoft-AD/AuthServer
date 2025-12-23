
namespace SharedKernel.Models;

public sealed class ClientView
{
    public required string ClientId { get; init; }

    public string? DisplayName { get; init; }

    public string? ClientType { get; init; }

    public IReadOnlyList<string> RedirectUris { get; init; } = [];

    public IReadOnlyList<string> Scopes { get; init; } = [];

    public IReadOnlyList<string> GrantTypes { get; init; } = [];
}
