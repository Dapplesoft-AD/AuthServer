using Application.Abstractions.Messaging;

namespace Application.Businesses.Delete;

public sealed record DeleteBusinessCommand(Guid Id) : ICommand<Guid>;
