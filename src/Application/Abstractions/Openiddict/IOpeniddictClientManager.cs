
using SharedKernel.Models;

namespace Application.Abstractions.Openiddict;

public interface IOpeniddictClientManager
{
    Task<bool> IsAlreadyExist(string ClientId, CancellationToken ct);
    Task CreateAsync(ClientDescriptor descriptor, CancellationToken ct);
    Task DeleteAsync(string clientId, CancellationToken ct);
    Task<IReadOnlyList<ClientView>> GetAllAsync(CancellationToken ct);
}

