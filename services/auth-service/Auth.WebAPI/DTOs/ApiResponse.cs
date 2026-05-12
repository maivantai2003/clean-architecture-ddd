using Auth.Application.Common.Errors;

namespace Auth.WebAPI.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public Error? Error { get; init; }
    public T? Data { get; init; }
    public IDictionary<string, string[]>? ValidationErrors { get; init; }
    public string? TraceId { get; init; }
}
