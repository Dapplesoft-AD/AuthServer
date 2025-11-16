using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Businesses.Update;
public sealed record UpdateBusinessCommand(
    Guid Id,
    string BusinessName,
    string IndustryType,
#pragma warning disable CA1054 // URI-like parameters should not be strings
    string LogoUrl,
#pragma warning restore CA1054 // URI-like parameters should not be strings
    string Status
) : ICommand<Guid>;
