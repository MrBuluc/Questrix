using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace Questrix.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = GetStatusCode(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            ExceptionModel exceptionModel = new()
            {
                StatusCode = statusCode,
                Errors =
                    [
                        $"Hata Mesajı: {ex.Message}",
                        $"Mesaj Açıklaması: {ex.InnerException?.ToString()}"
                    ]
            };

            return context.Response.WriteAsync(exceptionModel.ToString());
        }

        private static int GetStatusCode(Exception ex) => ex switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status204NoContent,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
