using Application.Abstractions.Messaging;
using Application.Abstractions.Openiddict;
using SharedKernel;
using SharedKernel.Models;

namespace Application.ClientApps.Create;

public sealed record CreateClientCommandHandler(
    IOpeniddictClientManager ClientManager
    ) : ICommandHandler<CreateClientCommand>
{
    async Task<Result> ICommandHandler<CreateClientCommand>.Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {

        if (await ClientManager.IsAlreadyExist(command.ClientId, cancellationToken))
        {
            return Result.Failure($"Client with ClientId '{command.ClientId}' already exists.");
        }

        Uri[] redirectUris = [.. command.RedirectUris.Select(uri => new Uri(uri, "/signin-oidc"))];

        await ClientManager.CreateAsync(new ClientDescriptor
        {
            ClientId = command.ClientId,
            DisplayName = command.DisplayName,
            ClientSecret = command.ClientSecret,
            RedirectUris = redirectUris
        }, cancellationToken);

        return Result.Success();
    }
}
