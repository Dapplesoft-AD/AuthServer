using Application.Abstractions.Messaging;

namespace Application.Localities.Delete;

public sealed record DeleteLocalityCommand(Guid Id) : ICommand;
