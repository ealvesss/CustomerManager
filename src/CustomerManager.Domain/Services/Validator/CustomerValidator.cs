using CustomerManager.Domain.Entities;
using FluentValidation;

namespace CustomerManager.Domain.Services.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email should not be null!")
                .NotEmpty().WithMessage("Email should not be empty!");

            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name should not be null!")
                .NotEmpty().WithMessage("Name should not be empty!");
        }
    }
}
