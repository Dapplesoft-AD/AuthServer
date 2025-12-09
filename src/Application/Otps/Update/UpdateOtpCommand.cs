using Application.Abstractions.Messaging;

namespace Application.Otps.Update;

public sealed record UpdateOtpCommand(Guid OtpId) : ICommand<bool>;
