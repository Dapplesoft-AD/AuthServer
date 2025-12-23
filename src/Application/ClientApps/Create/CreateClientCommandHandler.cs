using Application.Abstractions.Messaging;
using Application.Abstractions.Openiddict;
using SharedKernel;
using SharedKernel.Models;

namespace Application.ClientApps.Create;

public sealed record CreateClientCommandHandler(
    IOpeniddictClientManager clientManager
    ) : ICommandHandler<CreateClientCommand>
{
    async Task<Result> ICommandHandler<CreateClientCommand>.Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {

        if (await clientManager.IsAlreadyExist(command.ClientId, cancellationToken))
        {
            return Result.Failure($"Client with ClientId '{command.ClientId}' already exists.");
        }

        Uri[] redirectUris = command.RedirectUris
            .Select(uri => new Uri(uri, "/signin-oidc"))
            .ToArray();

        await clientManager.CreateAsync(new ClientDescriptor
        {
            ClientId = command.ClientId,
            DisplayName = command.DisplayName,
            ClientSecret = command.ClientSecret,
            RedirectUris = redirectUris
        }, cancellationToken);

        return Result.Success();
    }
}
