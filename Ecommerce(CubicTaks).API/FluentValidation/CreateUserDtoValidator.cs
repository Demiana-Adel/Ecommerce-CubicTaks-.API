
using Ecommerce_CubicTaks_.Dto.Customer;
using Ecommerce_CubicTaks_.Dto.User;
using FluentValidation;
namespace Ecommerce_CubicTaks_.API.FluentValidation
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Role).NotEmpty().Must(role => role == "user" || role == "admin");
        }
    }
}
