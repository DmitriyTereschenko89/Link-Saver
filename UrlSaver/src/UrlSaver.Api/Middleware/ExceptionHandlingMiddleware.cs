using System;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace UrlSaver.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private const string MessageTemplate = "\nMessage:{Message}\nException:{Exception}";
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        private async Task ConvertExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();
            switch (exception)
            {
                case NullReferenceException nullReferenceException:
                    problemDetails.Status = StatusCodes.Status204NoContent;
                    problemDetails.Detail = nullReferenceException.Message;
                    context.Response.StatusCode = StatusCodes.Status204NoContent;
                    _logger.LogError(MessageTemplate, nullReferenceException.Message, nullReferenceException);
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    _logger.LogError(MessageTemplate, exception.Message, exception);
                    break;
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await ConvertExceptionAsync(context, exception);          
            }
        }
    }
}
