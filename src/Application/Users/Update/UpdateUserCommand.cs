using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Users;

namespace Application.Users.Update;

public sealed record UpdateUserCommand(
    Guid UserId,
    string? Fullname,
    string? Email,
    string? Password,
    string? Phone,
    Status? Status,
    bool? IsMFAEnabled,
    bool? IsEmailVarified
    ) : ICommand;

