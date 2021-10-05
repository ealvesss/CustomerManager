using CustomerManager.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Domain.Services.Validator
{
    public class FavoriteValidator: AbstractValidator<Favorite>
    {

        public FavoriteValidator()
        {
            RuleFor(f => f.CustomerId)
                .NotNull().WithMessage("CustomerId should not be null!")
                .NotEmpty().WithMessage("CustomerId should not be empty!");

            RuleFor(f => f.Products)
                .NotNull().WithMessage("Products should not be null!")
                .NotEmpty().WithMessage("Products should not be empty!");
        }
    }
}
