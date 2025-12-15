using Application.Abstractions.Messaging;

namespace Application.Projects.Get;
public sealed record GetProjectByIdQuery(Guid Id) : IQuery<GetProjectResponse>;
