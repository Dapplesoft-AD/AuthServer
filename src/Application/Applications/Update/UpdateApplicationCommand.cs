using Application.Abstractions.Messaging;
using Domain.Applications;

namespace Application.Applications.Update;
public sealed record UpdateApplicationCommand(
    Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    Uri RedirectUri,
    Uri ApiBaseUrl,
    Applicationapply.ApplicationStatus Status
) : ICommand<Guid>;
