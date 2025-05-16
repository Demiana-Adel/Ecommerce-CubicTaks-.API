
using Ecommerce_CubicTaks_.Dto.Customer;
using Ecommerce_CubicTaks_.Dto.User;
using FluentValidation;
using System;
namespace Ecommerce_CubicTaks_.API.FluentValidation
{
    public class CustomValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            // Add more validators here as needed
            if (validatorType == typeof(IValidator<CreateCustomerDto>))
            {
                return new CreateCustomerDtoValidator();
            }
            if (validatorType == typeof(IValidator<CreateUserDto>))
                return new CreateUserDtoValidator();
            if (validatorType == typeof(IValidator<UpdateCustomerDto>))
                return new UpdateCustomerDtoValidator();

          
         
            return null;
        }
    }
}