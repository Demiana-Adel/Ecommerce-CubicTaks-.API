
using Ecommerce_CubicTaks_.Dto.Customer;
using FluentValidation;
namespace Ecommerce_CubicTaks_.API.FluentValidation
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {

        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Address)
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10,15}$")
                .WithMessage("Phone number must be between 10 and 15 digits.");
        }
    }
}
