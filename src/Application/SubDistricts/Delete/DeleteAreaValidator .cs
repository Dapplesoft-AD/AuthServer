using System;
using FluentValidation;

namespace Application.SubDistricts.Delete;

public class DeleteAreaValidator : AbstractValidator<DeleteAreaCommand>
{
    public DeleteAreaValidator()
    {
        RuleFor(x => x.Id)
     .NotEmpty()
     .WithMessage("ID is required.")
     .NotEqual(Guid.Empty)
     .WithMessage("ID cannot be empty GUID.");
    }
}
