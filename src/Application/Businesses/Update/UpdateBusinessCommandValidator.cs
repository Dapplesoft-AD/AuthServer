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
            .IsInEnum()
            .WithMessage("Status must be either Active or Inactive.");
    }
}
