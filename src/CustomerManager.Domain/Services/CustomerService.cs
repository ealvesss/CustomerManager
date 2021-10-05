using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services
{
    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {

        private readonly ICustomerRepository _repository;
        private readonly IValidator<Customer> _validator;

        public CustomerService(IRepositoryBase<Customer> baseRepo, ICustomerRepository repository, IValidator<Customer> validator)
            : base(baseRepo, validator)
        {
            this._repository = repository;
            this._validator = validator;
        }

        public override async Task<ExecutionResult<Customer>> Create<TValidator>(Customer obj)
        {
            var validationResult = base.Validate(obj, _validator);

            if (!validationResult.IsValid)
                return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };

            var existsCustomer = await _repository.GetByEmail(obj.Email);

            if (existsCustomer != null)
            {
                validationResult.Errors.Add(new ValidationFailure("ExistsCustomer", "Customer with this email already exists!"));
                return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };
            }

            await _repository.Create(obj);

            return new ExecutionResult<Customer> { Data = obj, ValidationResult = validationResult };

        }

        public async Task<ExecutionResult<Customer>> Delete(Guid id)
        {
            var entity = await this._repository.GetById(id);

            if(entity == null)
            {
                var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("customerNotFound", "404") });
                return new ExecutionResult<Customer>() { Data = null, ValidationResult = validationResult };
            }

            await this._repository.Delete(entity);

            return new ExecutionResult<Customer>() { Data = null, ValidationResult = new ValidationResult() }; ;
        }

        public override Task<Customer> GetById(Guid id)
        {
            return this._repository.GetById(id);
        }

        public override async Task<ExecutionResult<Customer>> Update<TValidator>(Customer entity)
        {
            var validationResult = base.Validate(entity, this._validator);

            if (!validationResult.IsValid)
                return new ExecutionResult<Customer> { Data = entity, ValidationResult = validationResult };

            return await Task.FromResult(new ExecutionResult<Customer> { Data = entity, ValidationResult = validationResult });
        }
    }
}
