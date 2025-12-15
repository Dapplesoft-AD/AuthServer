using Application.Abstractions.Messaging;
using Domain.Projects;

namespace Application.Projects.Update;

public sealed record UpdateProjectCommand(
    Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    string Domain,
    string RedirectUrl,
    ProjectStatus Status
) : ICommand<Guid>;
