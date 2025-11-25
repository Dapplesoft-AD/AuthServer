using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Delete;

public sealed record DeleteBusinessMemberCommand(Guid Id) : ICommand<Guid>;
