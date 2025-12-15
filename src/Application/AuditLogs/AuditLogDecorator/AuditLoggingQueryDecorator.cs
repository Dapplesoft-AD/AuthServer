using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.AuditLogs.AuditLogDecorator;

public class AuditLoggingQueryDecorator<TRequest, TResponse>
    : AuditLoggingBase<TRequest>, IQueryHandler<TRequest, TResponse>
    where TRequest : class, IQuery<TResponse>  
{
    private readonly IQueryHandler<TRequest, TResponse> _inner;

    public AuditLoggingQueryDecorator(
        IQueryHandler<TRequest, TResponse> inner,
        IUserContext userContext,
        IApplicationDbContext dbContext,
        ILogger<AuditLoggingQueryDecorator<TRequest, TResponse>> logger)
        : base(userContext, dbContext, logger)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public async Task<Result<TResponse>> Handle(TRequest query, CancellationToken cancellationToken)
    {
        // Execute the query first
        Result<TResponse> result = await _inner.Handle(query, cancellationToken);
        
            try
            {
                await CreateAuditLogEntryAsync(query, cancellationToken);
            }
            catch (Exception ex)
            {
                // Don't fail the query if audit logging fails
                _logger.LogWarning(ex, "Failed to create audit log for query {QueryName}", typeof(TRequest).Name);
            }
        

        return result;
    }
}
