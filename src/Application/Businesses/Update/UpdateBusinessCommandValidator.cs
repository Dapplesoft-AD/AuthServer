using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Businesses.Update;
public sealed class UpdateBusinessCommandValidator : AbstractValidator<UpdateBusinessCommand>
{
    public UpdateBusinessCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Business Id is required.");
        RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Business Name is required.");
        RuleFor(x => x.IndustryType).NotEmpty().WithMessage("Industry Type is required.");
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required.")
            .Must(s => s == "active" || s == "inactive")
            .WithMessage("Status must be either 'active' or 'inactive'.");
    }
}
