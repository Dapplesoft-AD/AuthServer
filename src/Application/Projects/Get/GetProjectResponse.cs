using Domain.Projects;

namespace Application.Projects.Get;

public sealed record GetProjectResponse(
    Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    string Domain,
    string RedirectUrl,
    ProjectStatus Status);
