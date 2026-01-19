namespace User.WebAPI.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }

    private ApiResponse() { }

    public static ApiResponse<T> Ok(T data, string? message = null)
        => new()
        {
            Success = true,
            Data = data,
            Message = message
        };

    public static ApiResponse<T> Fail(
        string message,
        List<string>? errors = null)
        => new()
        {
            Success = false,
            Message = message,
            Errors = errors
        };
}
