using System;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
        {
            return app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run( httpContext =>
                {
                    var error = httpContext
                        .Features
                        .Get<IExceptionHandlerFeature>()
                        ?.Error;
                    return error switch
                    {
                        EntityNotFoundException entityNotFoundException => HandleEntityNotFoundException(entityNotFoundException, httpContext),
                        _ => HandleUnknownException(error, httpContext, environment)
                    };
                });
            });
        }

        private static async Task HandleEntityNotFoundException(EntityNotFoundException entityNotFoundException, HttpContext httpContext)
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Detail = entityNotFoundException.Message,
                Instance = string.Empty,
                Status = StatusCodes.Status404NotFound,
                Title = "A resource was not found",
                Type = $"https://httpstatuses.com/{StatusCodes.Status404NotFound}",
            };
            // ProblemDetails has it's own content type
            httpContext.Response.ContentType = "application/problem+json";
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problemDetails);
        }

        private static async Task HandleUnknownException(Exception exception, HttpContext httpContext, IHostEnvironment environment)
        {
            if(exception == null) // should never be null, but to be safe
            {
                httpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            }

            var problemDetails = new ProblemDetails()
            {
                Instance = string.Empty,
                Status = StatusCodes.Status500InternalServerError,
                Type = $"https://httpstatuses.com/{StatusCodes.Status500InternalServerError}"
            };

            if (environment.IsDevelopment())
            {
                problemDetails.Detail = exception.StackTrace;
                problemDetails.Title = exception.Message;
            }
            else
            {
                problemDetails.Detail = string.Empty;
                problemDetails.Title = "An unexpected server fault occurred";
            }

            // ProblemDetails has it's own content type
            httpContext.Response.ContentType = "application/problem+json";
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problemDetails);
        }
    }
}
