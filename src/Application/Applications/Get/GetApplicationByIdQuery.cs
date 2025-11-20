using Application.Abstractions.Messaging;

namespace Application.Applications.Get;
public sealed record GetApplicationByIdQuery(Guid Id) : IQuery<ApplicationResponse>;
