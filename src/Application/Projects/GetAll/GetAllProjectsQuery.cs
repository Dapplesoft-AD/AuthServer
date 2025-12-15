using Application.Abstractions.Messaging;
using Application.Projects.Get;

namespace Application.Projects.GetAll;
public sealed record GetAllProjectsQuery() : IQuery<List<GetAllProjectsResponse>>;
