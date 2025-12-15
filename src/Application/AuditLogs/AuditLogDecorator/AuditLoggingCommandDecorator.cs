using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.AuditLogs.AuditLogDecorator;

public class AuditLoggingCommandDecorator<TRequest, TResponse>
    : AuditLoggingBase<TRequest>, ICommandHandler<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>  // Add 'class' constraint here
{
    private readonly ICommandHandler<TRequest, TResponse> _inner;

    public AuditLoggingCommandDecorator(
        ICommandHandler<TRequest, TResponse> inner,
        IUserContext userContext,
        IApplicationDbContext dbContext,
        ILogger<AuditLoggingCommandDecorator<TRequest, TResponse>> logger)
        : base(userContext, dbContext, logger)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public async Task<Result<TResponse>> Handle(TRequest command, CancellationToken cancellationToken)
    {
        Result<TResponse> result = await _inner.Handle(command, cancellationToken);

        if (result.IsSuccess)
        {
            try
            {
                await CreateAuditLogEntryAsync(command, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to create audit log for command {CommandName}", typeof(TRequest).Name);
            }
        }

        return result;
    }
}
