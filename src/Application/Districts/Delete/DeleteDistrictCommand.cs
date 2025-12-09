using System;
using Application.Abstractions.Messaging;

namespace Application.Districts.Delete;

public sealed record DeleteDistrictCommand(Guid Id) : ICommand;
