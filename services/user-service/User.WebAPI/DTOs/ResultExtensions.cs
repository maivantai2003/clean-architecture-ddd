using User.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace User.WebAPI.DTOs
{
    public static class ResultExtensions
    {
        public static IActionResult ToApiResponse<T>(this Result<T> result,ControllerBase controller){
            var apiResponse = result.IsSuccess?ApiResponse<T>.Ok(result.Value!): ApiResponse<T>.Fail(result.Error!);
            return controller.StatusCode((int)result.Status,apiResponse);
        }
    }
}
