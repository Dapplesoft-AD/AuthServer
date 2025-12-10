using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.OtpSms;

public sealed record SmsOtpCommand(string PhoneNumber) : ICommand<Guid>;
