using Auth.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.DTOs
{
    public static class ResultExtensions
    {
        public static IActionResult ToApiResponse<T>(this Result<T> result,ControllerBase controller)
        {
            var response = result.IsSuccess? ApiResponse<T>.Ok(result.Value!): ApiResponse<T>.Fail(result.Error!, result.Errors);
            return controller.StatusCode((int)result.Status, response);
        }
    }
}
