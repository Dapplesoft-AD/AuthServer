using Application.Abstractions.Messaging;
using Application.Applications.Get;

namespace Application.Applications.GetAll;
public sealed record GetAllApplicationsQuery() : IQuery<List<ApplicationResponse>>;
