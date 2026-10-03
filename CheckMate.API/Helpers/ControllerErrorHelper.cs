using CheckMate.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Helpers
{
    public static class ControllerErrorHelper
    {
        public static IActionResult ToActionResult(
            this Error error,
            ControllerBase controller)
        {
            var (status, title) = error.Type switch
            {
                ErrorTypeEnum.NOT_FOUND =>
                    (StatusCodes.Status404NotFound, "Resource not found"),

                ErrorTypeEnum.CONFLICT =>
                    (StatusCodes.Status409Conflict, "Conflict"),

                ErrorTypeEnum.VALIDATION =>
                    (StatusCodes.Status400BadRequest, "Validation error"),

                ErrorTypeEnum.FORBIDDEN =>
                    (StatusCodes.Status403Forbidden, "Forbidden"),

                _ =>
                    (StatusCodes.Status500InternalServerError,
                     "An unexpected error occurred")
            };

            return controller.Problem(
                statusCode: status,
                title: title,
                detail: status == StatusCodes.Status500InternalServerError
                    ? "An unexpected error occurred."
                    : error.Message,
                instance: controller.Request.Path,
                extensions: new Dictionary<string, object?>
                {
                    ["code"] = error.Code
                });
        }
    }
}
