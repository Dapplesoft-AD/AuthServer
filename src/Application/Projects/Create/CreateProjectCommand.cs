using Application.Abstractions.Messaging;
using Domain.Projects;

namespace Application.Projects.Create;
public sealed record CreateProjectCommand(
    string Name,
    string ClientId,
    string ClientSecret,
    string Domain,
    string RedirectUrl,
    ProjectStatus Status
) : ICommand<Guid>;
