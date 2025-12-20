using Application.Abstractions.Messaging;

namespace Application.ClientApps.Create;

public sealed record CreateClientCommand(
    string ClientId,
    string DisplayName,
    string ClientSecret,
    Uri[] RedirectUri
) : ICommand;
