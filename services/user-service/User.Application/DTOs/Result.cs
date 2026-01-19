
using User.Application.Common;

namespace User.Application.DTOs
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public HttpStatus Status { get; }

        protected Result(bool isSuccess, HttpStatus status, string? error)
        {
            IsSuccess = isSuccess;
            Status = status;
            Error = error;
        }

        public static Result Success(HttpStatus status = HttpStatus.Ok)
            => new(true, status, null);

        public static Result Failure(
            string error,
            HttpStatus status = HttpStatus.BadRequest)
            => new(false, status, error);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(
            bool isSuccess,
            HttpStatus status,
            T? value,
            string? error)
            : base(isSuccess, status, error)
        {
            Value = value;
        }

        public static Result<T> Success(
            T value,
            HttpStatus status = HttpStatus.Ok)
            => new(true, status, value, null);

        public static new Result<T> Failure(
            string error,
            HttpStatus status = HttpStatus.BadRequest)
            => new(false, status, default, error);
    }
}
