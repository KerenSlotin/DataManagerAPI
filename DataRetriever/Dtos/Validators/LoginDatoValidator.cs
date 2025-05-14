using FluentValidation;

namespace DataRetriever.Dtos.Validators
{
  internal class LoginDtoValidator : AbstractValidator<LoginDto>
  {
    public LoginDtoValidator()
    {
      RuleFor(x => x.Username)
          .NotEmpty().WithMessage("Username must not be empty.")
          .MaximumLength(15).WithMessage("Username must be 15 characters or fewer.");

      RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password must not be empty.")
          .MaximumLength(12).WithMessage("Password must be 12 characters or fewer.");
    }
  }
}