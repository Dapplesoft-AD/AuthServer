using Application.Abstractions.Messaging;

namespace Application.Applications.Delete;
public sealed record DeleteApplicationCommand(Guid Id) : ICommand;
