using System.Data.Entity.Core;

using Microsoft.AspNetCore.Mvc;

using UrlSaver.Api.Exceptions;

namespace UrlSaver.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        private async Task ConvertExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();
            switch (exception)
            {

                case BadHttpRequestException badRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = badRequestException.Message;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    _logger.LogError(badRequestException, "Exception occured.");
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Detail = unauthorizedAccessException.Message;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    _logger.LogError(unauthorizedAccessException, "Exception occured.");
                    break;
                case ForbiddenException forbiddenException:
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Detail = forbiddenException.Message;
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    _logger.LogError(forbiddenException, "Exception occured.");
                    break;
                case ObjectNotFoundException objectNotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = objectNotFoundException.Message;
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    _logger.LogError(objectNotFoundException, "Exception occured.");
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    _logger.LogError(exception, "Exception occured.");
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
