using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Token.Delete;

internal sealed class DeleteTokenCommandValidator : AbstractValidator<DeleteTokenCommand>
{
    public DeleteTokenCommandValidator()
    {
        RuleFor(x => x.TokenId)
            .NotEmpty().WithMessage("Token ID is required.");
    }
}
