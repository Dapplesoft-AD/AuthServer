using Application.Abstractions.Messaging;

namespace Application.SubDistricts.Delete;

public sealed record DeleteAreaCommand(Guid Id) : ICommand;
