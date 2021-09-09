using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services
{
    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {

        private readonly IRepositoryBase<Customer> _baseRepository;
        private readonly IValidator<Customer> _validator;

        public CustomerService(IRepositoryBase<Customer> baseRepository, IValidator<Customer> validator)
            : base(baseRepository, validator)
        {
            this._baseRepository = baseRepository;
            this._validator = validator;
        }

        public override async Task<ExecutionResult<Customer>> Create<TValidator>(Customer obj)
        {
            var validationResult = base.Validate(obj, _validator);

            if (!validationResult.IsValid)
                return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };

            var existsCustomer = await _baseRepository.GetBy(c => c.Email == obj.Email);

            if (existsCustomer != null)
            {
                validationResult.Errors.Add(new ValidationFailure("ExistsCustomer", "Customer with this email already exists!"));
                return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };
            }

            await _baseRepository.Create(obj);

            return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };

        }

        public override async Task Delete(Customer entity)
        {
            await _baseRepository.Delete(entity);
        }

        public override async Task<IEnumerable<Customer>> Get()
        {
            return await _baseRepository.Get();
        }


        public override async Task<Customer> GetBy(Expression<Func<Customer, bool>> expression)
        {
            return await _baseRepository.GetBy(expression);
        }

        public override Task<ExecutionResult<Customer>> Update<TValidator>(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
