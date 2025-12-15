using Domain.Projects;

namespace Application.Projects.GetAll;

public sealed record GetAllProjectsResponse(
    Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    string RedirectUrl,
    ProjectStatus Status);
