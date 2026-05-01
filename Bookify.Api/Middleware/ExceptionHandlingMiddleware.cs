using System.ComponentModel.DataAnnotations;

namespace Bookify.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured : {Message}", ex.Message);
                var exceptionDetails = GetExceptionDetails(ex);
                var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = exceptionDetails.StatusCode,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail
                };
                if (exceptionDetails.Error is not null)
                {
                    problemDetails.Extensions["error"] = exceptionDetails.Error;
                }
                context.Response.StatusCode = exceptionDetails.StatusCode;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception ex)
        {
            return ex switch
            {
                ValidationException validationException => new ExceptionDetails(StatusCodes.Status400BadRequest, "ValidationFailure", "Validation error", "One or more validation error has occured", new[] { validationException.Message }),
                                                      _ => new ExceptionDetails(StatusCodes.Status500InternalServerError, "ServerError", "An unexcepected error has occured", ex.Message, null)
            };
        }


        internal record ExceptionDetails(int StatusCode, string Type, string Title, string Detail, IEnumerable<object>? Error);
    }
}
