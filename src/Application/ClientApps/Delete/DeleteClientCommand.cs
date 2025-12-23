using Application.Abstractions.Messaging;

namespace Application.ClientApps.Delete;

public sealed record DeleteClientCommand(string ClientId) : ICommand;
