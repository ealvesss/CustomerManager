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
    public class ServiceBase<T> : IServiceBase<T> where T : EntityBase
    {
        private readonly IRepositoryBase<T> _baseRepository;
        private readonly IValidator<T> _validator;

        public ServiceBase(IRepositoryBase<T> baseRepository, IValidator<T> validator)
        {
            _baseRepository = baseRepository;
            _validator = validator;
        }


        public virtual Task<ExecutionResult<T>> Create<TValidator>(T obj)
        {
            var validationResult = Validate(obj, _validator);

            if (!validationResult.IsValid)
                return default;

            _baseRepository.Create(obj);

            var result = new ExecutionResult<T> { Data = obj, ValidationResult = validationResult };

            return Task.FromResult(result);
        }

        public virtual Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetBy(Expression<Func<T,bool>> expression) => await _baseRepository.GetBy(expression);

        public virtual async Task<ExecutionResult<T>> Update<TValidator>(T obj)
        {
            var result = Validate(obj, _validator);

            if (!result.IsValid)
                return default;

            await _baseRepository.Update(obj);

            return new ExecutionResult<T> { Data = obj, ValidationResult = result };
        }

        public virtual Task<IEnumerable<T>> Get()
        {
            //var result = _baseRepository.Get();
            //return result;
            throw new NotImplementedException();
        }
       
       
        public virtual ValidationResult Validate(T obj, IValidator<T> validator)
        {
            if (obj == null)
                throw new NullReferenceException("Object is Null " + typeof(T));

            return validator.Validate(obj);
        }

    }
}
