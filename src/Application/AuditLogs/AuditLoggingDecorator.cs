using System.Reflection;
using System.Text.Json;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using SharedKernel;

namespace Application.AuditLogs;

public class AuditLoggingDecorator<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly ICommandHandler<TRequest, TResponse> _inner;
    private readonly IUserContext _userContext;
    private readonly IApplicationDbContext _db;

    public AuditLoggingDecorator(
        ICommandHandler<TRequest, TResponse> inner,
        IUserContext userContext,
        IApplicationDbContext db)
    {
        _inner = inner;
        _userContext = userContext;
        _db = db;
    }

    public async Task<Result<TResponse>> Handle(TRequest command, CancellationToken cancellationToken)
    {
        // 1️⃣ Execute the actual command handler
        Result<TResponse> result = await _inner.Handle(command, cancellationToken);

        // 2️⃣ Skip audit if command failed
        if (!result.IsSuccess)
        {
            return result;
        }

        // 3️⃣ Determine userId (allow anonymous)
        Guid? userId = _userContext.UserId != Guid.Empty ? _userContext.UserId : null;

        // 4️⃣ Determine businessId (if exists in command, otherwise Guid.Empty)
        Guid businessId = TryGetBusinessId(command);

        // 5️⃣ Create audit log
        var audit = new AuditLog
        {
            Id = Guid.NewGuid(),       // generate new GUID for audit log
            UserId = userId,           // nullable for anonymous users
            BusinessId = businessId,
            Action = typeof(TRequest).Name,
            Description = JsonSerializer.Serialize(command),
            CreatedAt = DateTime.UtcNow
        };

        await _db.AuditLogs.AddAsync(audit, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return result;
    }

    private Guid TryGetBusinessId(TRequest command)
    {
        PropertyInfo? prop = command.GetType().GetProperty("BusinessId", BindingFlags.Public | BindingFlags.Instance);

        if (prop == null)
        {
            return Guid.Empty; // No BusinessId in command
        }

        object? value = prop.GetValue(command);

        return value is Guid guid ? guid : Guid.Empty;
    }
}





//using System.Reflection;
//using System.Text.Json;
//using Application.Abstractions.Authentication;
//using Application.Abstractions.Data;
//using Application.Abstractions.Messaging;
//using Domain.AuditLogs;
//using SharedKernel;

//namespace Application.AuditLogs;

//public class AuditLoggingDecorator<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
//    where TRequest : ICommand<TResponse>
//{
//    private readonly ICommandHandler<TRequest, TResponse> _inner;
//    private readonly IUserContext _userContext;
//    private readonly IApplicationDbContext _db;

//    public AuditLoggingDecorator(
//        ICommandHandler<TRequest, TResponse> inner,
//        IUserContext userContext,
//        IApplicationDbContext db)
//    {
//        _inner = inner;
//        _userContext = userContext;
//        _db = db;
//    }

//    public async Task<Result<TResponse>> Handle(TRequest command, CancellationToken cancellationToken)
//    {
//        // 1️⃣ Execute the actual command handler
//        Result<TResponse> result = await _inner.Handle(command, cancellationToken);

//        // 2️⃣ Skip audit if command failed
//        if (!result.IsSuccess)
//        {
//            return result;
//        }

//        // 3️⃣ Skip audit if user is not authenticated
//        if (_userContext.UserId == Guid.Empty)
//        {
//            return result;
//        }

//        // 4️⃣ Create audit log
//        var audit = new AuditLog
//        {
//            UserId = _userContext.UserId,
//            BusinessId = TryGetBusinessId(command),
//            Action = typeof(TRequest).Name,
//            Description = JsonSerializer.Serialize(command), // safe JSON snapshot
//            CreatedAt = DateTime.UtcNow
//        };

//        await _db.AuditLogs.AddAsync(audit, cancellationToken);
//        await _db.SaveChangesAsync(cancellationToken);

//        return result;
//    }

//    private Guid TryGetBusinessId(TRequest command)
//    {
//        PropertyInfo? prop = command.GetType().GetProperty("BusinessId", BindingFlags.Public | BindingFlags.Instance);

//        if (prop == null)
//        {
//            return Guid.Empty;
//        }

//        object? value = prop.GetValue(command);

//        return value is Guid guid ? guid : Guid.Empty;
//    }
//}
