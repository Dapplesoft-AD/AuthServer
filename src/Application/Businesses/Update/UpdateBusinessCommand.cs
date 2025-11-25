using Application.Abstractions.Messaging;
using Domain.Businesses;

namespace Application.Businesses.Update;

public sealed record UpdateBusinessCommand(
    Guid Id,
    string BusinessName,
    string IndustryType,
    Uri LogoUrl,
    Status Status) : ICommand;
