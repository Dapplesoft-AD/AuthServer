using Application.Abstractions.Openiddict;
using Application.Abstractions.Messaging;
using Application.ClientApps.Create;
using SharedKernel;

namespace Application.ClientApps.Delete;

internal class DeleteClientCommandHandler(
    IOpeniddictClientManager clientManager
    ) : ICommandHandler<DeleteClientCommand>
{
    public async Task<Result> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        if (!await clientManager.IsAlreadyExist(command.ClientId, cancellationToken))
        {
            return Result.Failure($"Client with id '{command.ClientId}' does not exist.");
        }

        await clientManager.DeleteAsync(command.ClientId, cancellationToken);
        return Result.Success();
    }
}
