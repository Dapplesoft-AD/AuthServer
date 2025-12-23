using Application.Abstractions.Messaging;
using Application.Abstractions.Openiddict;
using SharedKernel;
using SharedKernel.Models;

namespace Application.ClientApps.GetAll;

public class GetAllClientQueryHandler(
    IOpeniddictClientManager clientManager
    ) : IQueryHandler<GetAllClientQuery, IReadOnlyList<ClientView>>
{
    public async Task<Result<IReadOnlyList<ClientView>>> Handle(GetAllClientQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ClientView> clients = await clientManager.GetAllAsync(cancellationToken);
            return Result.Success(clients);
        }
        catch (Exception ex)
        {
            return (Result<IReadOnlyList<ClientView>>)Result.Failure($"An error occurred: {ex.Message}");
        }
    }
}
