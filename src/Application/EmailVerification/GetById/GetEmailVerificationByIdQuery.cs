using Application.Abstractions.Messaging;

namespace Application.EmailVerification.GetById;

public sealed record GetEmailVerificationByIdQuery(Guid EV_Id) : IQuery<EmailVerificationResponse>;
