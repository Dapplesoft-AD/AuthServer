using System.Reflection;
using System.Text.Json;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.AuditLogs;
using Microsoft.Extensions.Logging;

namespace Application.AuditLogs;

public static class AuditLogSerializerOptions
{
    public static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };
}

public abstract class AuditLoggingBase<TRequest> where TRequest : class
{
    protected readonly IUserContext _userContext;
    protected readonly IApplicationDbContext _dbContext;
    protected readonly ILogger _logger;

    protected AuditLoggingBase(
        IUserContext userContext,
        IApplicationDbContext dbContext,
        ILogger logger)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected async Task CreateAuditLogEntryAsync(TRequest request, CancellationToken cancellationToken)
    {
        Guid userId = GetUserIdOrDefault();
        Guid businessId = ExtractBusinessId(request);
        string actionName = typeof(TRequest).Name;

        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BusinessId = businessId,
            Action = actionName,
            Description = SerializeRequestSafely(request),
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.AuditLogs.AddAsync(auditLog, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogDebug(
            "Audit log created for {Action} by user {UserId}",
            actionName,
            userId == Guid.Empty ? "Anonymous" : userId.ToString());
    }

    protected Guid GetUserIdOrDefault()
    {
        try
        {
            // Safe navigation with null check
            if (_userContext?.UserId is not { } userId || userId == Guid.Empty)
            {
                return Guid.Empty;
            }

            return userId;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Failed to get user ID from context, treating as anonymous");
            return Guid.Empty;
        }
    }

    protected Guid ExtractBusinessId(TRequest request)
    {
        if (request is null)
        {
            return Guid.Empty;
        }

        try
        {
            PropertyInfo? businessIdProperty = request.GetType().GetProperty(
                "BusinessId",
                BindingFlags.Public | BindingFlags.Instance);

            if (businessIdProperty is null)
            {
                return Guid.Empty;
            }

            object? value = businessIdProperty.GetValue(request);

            return value is Guid guidValue && guidValue != Guid.Empty ? guidValue : Guid.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(
                ex,
                "Failed to extract BusinessId from request {RequestName}",
                typeof(TRequest).Name);
            return Guid.Empty;
        }
    }

    protected string SerializeRequestSafely(TRequest request)
    {
        if (request is null)
        {
            return $"{{ \"Type\": \"{typeof(TRequest).Name}\", \"Error\": \"RequestIsNull\" }}";
        }

        try
        {          
            return JsonSerializer.Serialize(request, AuditLogSerializerOptions.Options);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "Failed to serialize request {RequestName} for audit logging",
                typeof(TRequest).Name);

            // Create a minimal safe JSON
            var safeObject = new
            {
                Type = typeof(TRequest).Name,
                Timestamp = DateTime.UtcNow.ToString("O"),
                Error = "SerializationFailed",
                ExceptionMessage = ex.Message
            };

            return JsonSerializer.Serialize(safeObject, AuditLogSerializerOptions.Options);
        }
    }
}
