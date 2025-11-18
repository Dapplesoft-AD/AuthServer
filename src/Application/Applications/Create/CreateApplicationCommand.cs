using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Applications;

namespace Application.Applications.Create;
public sealed record CreateApplicationCommand(string Name,
    string ClientId,
    string ClientSecret,
   Uri RedirectUri, Uri ApiBaseUrl,
    Applicationapply.ApplicationStatus Status
) : ICommand<Guid>;
