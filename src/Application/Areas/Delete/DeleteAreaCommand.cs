using System;
using Application.Abstractions.Messaging;

namespace Application.Areas.Delete;

public sealed record DeleteAreaCommand(Guid Id) : ICommand;
