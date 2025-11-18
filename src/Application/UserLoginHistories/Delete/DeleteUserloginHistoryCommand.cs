using Application.Abstractions.Messaging;

namespace Application.UserLoginHistories.Delete;
public sealed record DeleteUserloginHistoryCommand(Guid Id) : ICommand;
