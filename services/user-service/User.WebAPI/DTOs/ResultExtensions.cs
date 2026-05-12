using Microsoft.AspNetCore.Mvc;
using User.Application.Common.Errors;
using User.WebAPI.Common;

namespace User.WebAPI.DTOs
{
    public static class ResultExtensions
    {
        public static IActionResult ToApiResponse<T>(this Result<T> result, ControllerBase controller)
        {
            //var response = result.IsSuccess? ApiResponse<T>.Ok(result.Value!): ApiResponse<T>.Fail(result.Error!, result.Errors);
            //return controller.StatusCode((int)result.Status, response);
            if (result.IsSuccess)
            {
                return controller.Ok(new ApiResponse<T>
                {
                    Success = true,
                    Data = result.Value,
                    TraceId = controller.HttpContext.TraceIdentifier
                });
            }
            var statusCode = ErrorMappings.GetStatusCode(result.Error!);
            return controller.StatusCode(
                statusCode,
                new ApiResponse<T>
                {
                    Success = false,
                    Error = result.Error,
                    ValidationErrors = result.ValidationErrors,
                    TraceId = controller.HttpContext.TraceIdentifier
                });
        }
    }
}
