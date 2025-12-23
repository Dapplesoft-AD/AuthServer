
namespace SharedKernel.Models;

public sealed class ClientDescriptor
{
    public required string ClientId { get; init; }

    public required string DisplayName { get; init; }

    public required string ClientSecret { get; init; }

    public required IReadOnlyList<Uri> RedirectUris { get; init; }
}
