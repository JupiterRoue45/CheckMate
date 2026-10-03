using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Common.Results
{
    internal class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access the value of a failed result.");
        protected Result(bool isSuccess, T? value, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null);
        }
        public static new Result<T> Failure(Error error)
        {
            return new Result<T>(false, default, error);
        }
    }
    
}
