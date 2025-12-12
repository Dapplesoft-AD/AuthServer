using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.Delete;

public sealed record DeleteSmsConfigCommand(Guid SmsId) : ICommand;
