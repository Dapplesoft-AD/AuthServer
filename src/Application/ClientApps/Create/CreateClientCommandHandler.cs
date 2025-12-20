using System;
using System.Collections.Generic;
using System.Text;
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

        await clientManager.CreateAsync(new ClientDescriptor
        {
            ClientId = command.ClientId,
            DisplayName = command.DisplayName,
            ClientSecret = command.ClientSecret,
            RedirectUri = command.RedirectUri
        }, cancellationToken);

        return Result.Success();
    }
}
