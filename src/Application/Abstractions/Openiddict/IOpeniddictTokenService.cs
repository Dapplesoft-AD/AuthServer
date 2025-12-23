

namespace Application.Abstractions.Openiddict;

public interface IOpeniddictTokenService
{
    Task<IReadOnlyList<SharedKernel.Models.Token>> GetAllAsync(CancellationToken ct);
    Task DeleteAllAsync(CancellationToken ct);
    Task DeleteByIdAsync(string tokenId, CancellationToken ct);
}
