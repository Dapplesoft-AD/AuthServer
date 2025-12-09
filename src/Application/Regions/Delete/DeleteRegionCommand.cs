using System;
using Application.Abstractions.Messaging;

namespace Application.Regions.Delete;

public sealed record DeleteRegionCommand(Guid Id) : ICommand;
