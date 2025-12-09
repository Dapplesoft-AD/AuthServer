using Application.Abstractions.Messaging;

namespace Application.Countries.Delete;

public sealed record DeleteCountryCommand(Guid Id) : ICommand;

