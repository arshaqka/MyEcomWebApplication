using FluentValidation;
using MyEcomWebApplication.Application.Common.Models;

namespace MyEcomWebApplication.Application.Common.Validator
{
    public class CustomerOrderRequestValidator : AbstractValidator<CustomerOrderRequest>
    {
        public CustomerOrderRequestValidator()
        {
            RuleFor(x => x.User).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
