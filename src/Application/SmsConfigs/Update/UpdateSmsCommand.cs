using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.Update;

public sealed record UpdateSmsCommand(Guid SmsId, string SmsToken) : ICommand<Guid>;
