using FluentValidation;

namespace DataRetriever.Dtos.Validators
{
  internal class CreateDataDtoValidator : AbstractValidator<CreateDataDto>
  {
    public CreateDataDtoValidator()
    {
      RuleFor(x => x.Value)
          .NotEmpty().WithMessage("Value must not be empty.")
          .MaximumLength(100).WithMessage("Value must be 100 characters or fewer.");
    }
  }
}