using User.Application.Common;

namespace User.Application.Common.Errors
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }
        public IDictionary<string, string[]>? ValidationErrors { get; }

        protected Result(
            bool isSuccess,
            Error? error,
            IDictionary<string, string[]>? validationErrors = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ValidationErrors = validationErrors;
        }

        public static Result Success()
            => new(true, null);
        public static Result Failure(Error error)
            => new(false, error);

        public static Result ValidationFailure(
        IDictionary<string, string[]> errors)
        => new(false, new Error("Validation.Invalid", "Validation failed"), errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(
            bool isSuccess,
            T? value,
            Error? error,
            IDictionary<string, string[]>? validationErrors = null)
            : base(isSuccess, error, validationErrors)
        {
            Value = value;
        }
        public static Result<T> Success(T value)
            => new(true, value, null);

        public static new Result<T> Failure(Error error)
            => new(false, default, error);

        public static Result<T> ValidationFailure(
            IDictionary<string, string[]> errors)
            => new(
                false,
                default,
                new Error(
                    "Validation.Invalid",
                    "Validation failed"),
                errors);
    }
}
