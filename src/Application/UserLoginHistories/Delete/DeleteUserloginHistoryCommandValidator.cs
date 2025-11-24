using FluentValidation;

namespace Application.UserLoginHistories.Delete;

internal sealed class DeleteUserloginHistoryCommandValidator : AbstractValidator<DeleteUserloginHistoryCommand>
{
    public DeleteUserloginHistoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
