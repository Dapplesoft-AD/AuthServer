using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Openiddict;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Models;


namespace Application.Tokens.Delete;

internal sealed class DeleteTokenCommandHandler(
    IOpeniddictTokenService tokenService) : ICommandHandler<DeleteTokenCommand>
{
    public async Task<Result> Handle(DeleteTokenCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await tokenService.DeleteByIdAsync(command.TokenId, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                $"Failed to fetch tokens: {ex.Message}");
        }
    }
}
