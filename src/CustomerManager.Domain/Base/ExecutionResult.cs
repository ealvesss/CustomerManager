using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Domain.Base
{
    public class ExecutionResult<T>
    {
        public T Data { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
