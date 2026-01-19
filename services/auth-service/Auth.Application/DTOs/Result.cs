using Auth.Application.Common;

namespace Auth.Application.DTOs
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public IDictionary<string, string[]>? Errors { get; }
        public HttpStatus Status { get; }

        protected Result(
            bool isSuccess,
            HttpStatus status,
            string? error,
            IDictionary<string, string[]>? errors = null)
        {
            IsSuccess = isSuccess;
            Status = status;
            Error = error;
            Errors = errors;
        }

        public static Result Success(HttpStatus status = HttpStatus.Ok)
            => new(true, status, null);

        public static Result Failure(
            string error,
            HttpStatus status = HttpStatus.BadRequest)
            => new(false, status, error);

        public static Result ValidationFailure(
            IDictionary<string, string[]> errors,
            string error = "Validation failed",
            HttpStatus status = HttpStatus.BadRequest)
            => new(false, status, error, errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(
            bool isSuccess,
            HttpStatus status,
            T? value,
            string? error,
            IDictionary<string, string[]>? errors = null)
            : base(isSuccess, status, error, errors)
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

        public static Result<T> ValidationFailure(
            IDictionary<string, string[]> errors,
            string error = "Validation failed",
            HttpStatus status = HttpStatus.BadRequest)
            => new(false, status, default, error, errors);
    }
}
