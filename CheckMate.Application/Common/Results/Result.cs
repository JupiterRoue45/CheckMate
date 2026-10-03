using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("A result cannot be successful and contain an error.");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("A result cannot be a failure without an error.");
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }
    }
}
