using Application.Abstractions.Openiddict;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using SharedKernel.Models;

namespace Infrastructure.Openiddict;

public sealed class OpenIddictTokenService : IOpeniddictTokenService
{
    private readonly IOpenIddictTokenManager tokenManager;

    public OpenIddictTokenService(IOpenIddictTokenManager tokenManager)
    {
        this.tokenManager = tokenManager;
    }

    public Task DeleteAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(string tokenId, CancellationToken ct)
    {
        object? token = await tokenManager.FindByIdAsync(tokenId, ct);

        if (token is null)
        {
            return;
        }

        await tokenManager.DeleteAsync(token, ct);
    }


    public async Task<IReadOnlyList<Token>> GetAllAsync(CancellationToken ct)
    {
        var result = new List<Token>();

        await foreach (object token in tokenManager.ListAsync(null , null, ct))
        {
            result.Add(new Token
            {
                Id = await tokenManager.GetIdAsync(token, ct),
                Subject = await tokenManager.GetSubjectAsync(token, ct),
                ApplicationId = await tokenManager.GetApplicationIdAsync(token, ct),
                AuthorizationId = await tokenManager.GetAuthorizationIdAsync(token, ct),
                Type = await tokenManager.GetTypeAsync(token, ct),
                Status = await tokenManager.GetStatusAsync(token, ct),
                CreationDate = await tokenManager.GetCreationDateAsync(token, ct),
                ExpirationDate = await tokenManager.GetExpirationDateAsync(token, ct),
                Payload = await tokenManager.GetPayloadAsync(token, ct)
            });
        }

        return result;
    }
}
